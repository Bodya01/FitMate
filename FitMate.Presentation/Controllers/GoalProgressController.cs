﻿using FitMate.Application.Commands.GoalProgress;
using FitMate.Application.Queries.GoalProgress;
using FitMate.Business.Interfaces;
using FitMate.Controllers;
using FitMate.Core.UnitOfWork;
using FitMate.Presentation.Helpers;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Presentation.Controllers
{
    public class GoalProgressController : FitMateControllerBase
    {
        public GoalProgressController(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService) : base(mediator, unitOfWork, userService) { }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress([FromQuery] GetTimedProgressQuery query, CancellationToken cancellationToken)
        {
            query.UserId = await _userService.GetUserIdAsync(cancellationToken);
            var result = await _mediator.Send(query, cancellationToken);

            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress([FromQuery] GetWeightliftingProgressQuery query, CancellationToken cancellationToken)
        {
            query.UserId = await _userService.GetUserIdAsync(cancellationToken);
            var result = await _mediator.Send(query, cancellationToken);

            return Json(result);
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