using FitMate.Application.Commands.GoalProgress;
using FitMate.Business.Interfaces;
using FitMate.Controllers;
using FitMate.Core.UnitOfWork;
using FitMate.Presentation.Helpers;
using FitMate.Presentation.ViewModels.Goal;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Presentation.Controllers
{
    public class GoalProgressController : FitMateControllerBase
    {
        public GoalProgressController(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService) : base(mediator, unitOfWork, userService) { }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress(Guid goalId, CancellationToken cancellationToken)
        {
            //var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            //var progress = await _unitOfWork.GoalProgressRepository.Value
            //    .Get(e => /*e.GoalId == goalId &&*/ e.UserId == currentUserId, s => s)
            //    .OrderBy(x => x.Date)
            //    .ToListAsync(cancellationToken);

            //var result = progress
            //    .Select(record => new TimedProgressViewModel(
            //        Date: record.Date.ToString("d"),
            //        Timespan: ((TimedProgress)record).Time,
            //        Quantity: ((TimedProgress)record).Quantity,
            //        QuantityUnit: ((TimedProgress)record).QuantityUnit))
            //    .ToList();

            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress(Guid goalId, CancellationToken cancellationToken)
        {
            //var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            //var progress = await _unitOfWork.GoalProgressRepository.Value
            //    .Get(e => /*e.GoalId == goalId &&*/ e.UserId == currentUserId, s => s)
            //    .OrderBy(x => x.Date)
            //    .ToListAsync(cancellationToken);

            //var result = progress
            //    .Select(record => new WeightliftingProgressViewModel(
            //        Date: record.Date.ToString("d"),
            //        Weight: ((WeightliftingProgress)record).Weight,
            //        Reps: ((WeightliftingProgress)record).Reps))
            //    .ToList();

            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> AddTimedProgress([FromForm] CreateTimedProgressCommand command, CancellationToken cancellationToken)
        {
            command.UserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);
            return RedirectToAction(nameof(GoalController.ViewTimed), UiNamingHelper.GetControllerName<GoalController>(), new { Id = command.GoalId });
        }

        [HttpPost]
        public async Task<IActionResult> AddWeightliftingProgress([FromForm] CreateWeightliftingProgressCommand command, CancellationToken cancellationToken)
        {
            command.UserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);
            return RedirectToAction(nameof(GoalController.ViewWeightlifting), UiNamingHelper.GetControllerName<GoalController>(), new { Id = command.GoalId });
        }
    }
}