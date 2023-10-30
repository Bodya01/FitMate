using FitMate.Application.Commands.Goal;
using FitMate.Application.Queries.Goal;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.UI.Web.Controllers.Base;
using FitMate.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    public class GoalController : FitMateControllerBase
    {
        public GoalController(UserManager<FitnessUser> userManager, IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(userManager, mediator, unitOfWork, userService) { }

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var goals = await _unitOfWork.GoalRepository.Value
                .Get(e => e.UserId == currentUserId, s => s)
                .ToListAsync(cancellationToken);

            return View(goals);
        }

        [HttpGet]
        public IActionResult AddGoal()
        {
            var model = new WeightliftingGoal { Id = Guid.Empty };

            return View("editgoal", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditGoal(Guid id, CancellationToken cancellationToken)
        {
            var goal = await _mediator.Send(new GetGoalQuery(id), cancellationToken);

            if (goal is null) return BadRequest();

            return View(goal);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGoal(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteGoalCommand(id), cancellationToken);
            return RedirectToAction(nameof(GoalController.Summary));
        }

        [HttpPost]
        public async Task<IActionResult> EditGoal(EditGoalInputModel goalInput, CancellationToken cancellationToken)
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

            return RedirectToAction(nameof(GoalController.Summary));
        }

        [HttpPost]
        public async Task<IActionResult> AddProgress(AddGoalProgressInputModel progress, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(progress.GoalId, cancellationToken);

            if (goal is null) return BadRequest();

            GoalProgress newProgress;

            switch (progress.Type.ToLower())
            {
                case "weightlifting":
                    newProgress = new WeightliftingProgress()
                    {
                        Weight = progress.Weight,
                        Reps = progress.Reps
                    };
                    break;
                case "timed":
                    newProgress = new TimedProgress()
                    {
                        Time = new TimeSpan(progress.Hours, progress.Minutes, progress.Seconds),
                        Quantity = progress.Quantity
                    };
                    break;
                default:
                    return BadRequest();
            }

            newProgress.Goal = goal;
            newProgress.Date = progress.Date;
            newProgress.UserId = currentUserId;

            await _unitOfWork.GoalProgressRepository.Value.CreateAsync(newProgress, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction(nameof(GoalController.ViewGoal), new { ID = progress.GoalId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewGoal(Guid Id, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync();

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

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress(Guid goalId, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync();

            // Ascending order
            var progress = await _unitOfWork.GoalProgressRepository.Value
                .Get(e => e.GoalId == goalId && e.UserId == currentUserId, s => s)
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);
            // ----
            var result = Array.ConvertAll(progress.ToArray(), item => (WeightliftingProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight, Reps = record.Reps })
                .ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress(Guid goalId, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            // Ascending order
            var progress = await _unitOfWork.GoalProgressRepository.Value
                .Get(e => e.GoalId == goalId && e.UserId == currentUserId, s => s)
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);
            // ----
            var result = Array.ConvertAll(progress.ToArray(), item => (TimedProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Timespan = record.Time, Quantity = record.Quantity, QuantityUnit = record.QuantityUnit })
                .ToList();

            return Json(result);
        }
    }
}
