﻿using FitMate.Applcation.Commands.WorkoutPlan;
using FitMate.Applcation.Queries.WorkoutPlan;
using FitMate.Application.Commands.WorkoutPlan;
using FitMate.Infrastucture.Dtos;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    //TODO: Move all logic to handlers, implement validators
    public sealed class WorkoutController : FitMateControllerBase
    {
        public WorkoutController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var request = new GetWorkoutsForUser(_currentUserId);
            return View(await _mediator.Send(request, cancellationToken));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var newPlan = new WorkoutPlanDto(Guid.Empty, "Workout Plan", null);
            return View(newPlan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] GetWorkoutPlan query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            return View(await _mediator.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> Session([FromRoute] GetWorkoutPlan query, [FromRoute] int sessionId, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            var plan = await _mediator.Send(query, cancellationToken);

            if (sessionId < 0 || sessionId >= plan.Sessions.Count) return BadRequest();

            var session = plan.Sessions.ToList()[sessionId];
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateWorkoutPlan command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] EditWorkoutPlan command, CancellationToken cancellationToken)
        {
            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] DeleteWorkoutPlan query, CancellationToken cancellationToken)
        {
            query.UserId = _currentUserId;
            await _mediator.Send(query, cancellationToken);
            return RedirectToAction(nameof(Summary));
        }
    }
}