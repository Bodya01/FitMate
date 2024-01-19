using FitMate.Application.Commands.Goal;
using FitMate.Application.Queries.Goal;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.GoalProgress;
using FitMate.Infrastucture.Dtos.GoalProgress;
using FitMate.Infrastucture.Dtos.Goals;
using FitMate.Presentation.ViewModels.Goal;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    public sealed class GoalController : FitMateControllerBase
    {
        public GoalController(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(mediator, unitOfWork, userService) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);
            var result = await _mediator.Send(new GoalSummaryQuery(currentUserId), cancellationToken);

            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new WeightliftingGoalDto(Guid.Empty, string.Empty, string.Empty, new List<GoalProgressDto>(), default, default);
            return View("EditGoal", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);
            return View(await _mediator.Send(new GetGoalQuery(id, currentUserId), cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(Id, cancellationToken);

            if (goal is null) return BadRequest();

            var progress = await _unitOfWork.GoalProgressRepository.Value.Get(e => e.GoalId == Id && e.UserId == currentUserId, s => s).ToListAsync(cancellationToken);

            if (progress is null) return BadRequest();

            var viewModel = new GoalViewModel()
            {
                Goal = goal,
                Progress = progress
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateGoalInputModel model, CancellationToken cancellationToken)
        {
            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditGoalInputModel goalInput, CancellationToken cancellationToken)
        {
            if (!TryValidateModel(goalInput))
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(c => c.Any()).ToList();
                return BadRequest(errors);
            }

            Goal goal = null;

            if (goalInput.Id != Guid.Empty)
            {
                goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(goalInput.Id, cancellationToken);
            }
            else
            {
                switch (goalInput.Type.ToLower())
                {
                    case "weightlifting":
                        goal = new WeightliftingGoal();
                        break;
                    case "timed":
                        goal = new TimedGoal();
                        break;
                }
            }

            switch (goal)
            {
                case WeightliftingGoal wGoal:
                    wGoal.Reps = goalInput.Reps;
                    wGoal.Weight = goalInput.Weight;
                    break;
                case TimedGoal tGoal:
                    tGoal.Quantity = (int)goalInput.Quantity;
                    tGoal.QuantityUnit = goalInput.QuantityUnit;
                    tGoal.Time = new TimeSpan(goalInput.Hours, goalInput.Minutes, goalInput.Seconds);
                    break;
            }

            goal.Activity = goalInput.Activity;
            goal.UserId = await _userService.GetUserIdAsync(cancellationToken);

            if (goal.Id == Guid.Empty)
            {
                await _unitOfWork.GoalRepository.Value.CreateAsync(goal, cancellationToken);
            }
            else
            {
                await _unitOfWork.GoalRepository.Value.UpdateAsync(goal, cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(new DeleteGoalCommand(id, currentUserId), cancellationToken);
            return RedirectToAction(nameof(Summary));
        }
    }
}