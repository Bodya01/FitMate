using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Presentation.ViewModels.Goal;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using FitMate.Infrastructure.Models.GoalProgress;
using Microsoft.EntityFrameworkCore;
using FitMate.Controllers;
using FitMate.Presentation.Helpers;
using FitMate.Infrastructure.Entities.Interfaces;

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
        public async Task<IActionResult> AddProgress(AddGoalProgressInputModel progress, CancellationToken cancellationToken)
        {
            //var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            //var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(progress.GoalId, cancellationToken);

            //if (goal is null) return BadRequest();

            //IGoalProgress newProgress;

            //switch (progress.Type.ToLower())
            //{
            //    case "weightlifting":
            //        newProgress = new WeightliftingProgress()
            //        {
            //            Weight = progress.Weight,
            //            Reps = progress.Reps
            //        };
            //        break;
            //    case "timed":
            //        newProgress = new TimedProgress()
            //        {
            //            Time = new TimeSpan(progress.Hours, progress.Minutes, progress.Seconds),
            //            Quantity = progress.Quantity
            //        };
            //        break;
            //    default:
            //        return BadRequest();
            //}

            ////newProgress.Goal = goal;
            //newProgress.Date = progress.Date;
            //newProgress.UserId = currentUserId;

            //await _unitOfWork.GoalProgressRepository.Value.CreateAsync(newProgress, cancellationToken);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction(nameof(GoalController.View), UiNamingHelper.GetControllerName<GoalController>(), new { Id = progress.GoalId });
        }
    }
}