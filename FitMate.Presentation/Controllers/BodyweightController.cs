﻿using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Application.Commands.BodyweightTarget;
using FitMate.Application.Queries.Bodyweight;
using FitMate.Application.Queries.BodyweightRecord;
using FitMate.Presentation.ViewModels.Bodyweight;
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
    public sealed class BodyweightController : FitMateControllerBase
    {
        public BodyweightController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var (target, records) = await _mediator.Send(new GetBodyweightSummary(_currentUserId), cancellationToken);
            return View(BodyweightSummaryViewModel.Create(records, target));
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget(CancellationToken cancellationToken) =>
            View(await _mediator.Send(new GetCurrentBodyweightTarget(_currentUserId), cancellationToken));

        [HttpGet]
        public async Task<IActionResult> EditRecords(CancellationToken cancellationToken) =>
            View(await _mediator.Send(new GetBodyweightRecords(_currentUserId), cancellationToken));

        [HttpGet]
        public async Task<IActionResult> GetBodyweightData([FromQuery] int previousDays, CancellationToken cancellationToken)
        {
            var query = new GetBodyweightRecords(_currentUserId, DateTime.Today.AddDays(-previousDays), DateTime.Today, false);
            var records = await _mediator.Send(query, cancellationToken);

            var result = records
                .OrderBy(x => x.Date)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight })
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget([FromBody] EditBodyweightTarget command, CancellationToken cancellationToken)
        {
            if (command.Weight <= 0 || command.Weight >= 200 || command.Date <= DateTime.Today) return BadRequest();

            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm] EditBodyweightRecords command, CancellationToken cancellationToken)
        {
            if (command.Dates is not null && command.Weights is not null)
            {
                if (command.Dates.Length != command.Weights.Length
                || command.Weights.Any(x => x <= 0 || x >= 200))
                {
                    return BadRequest();
                }
            }

            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight([FromForm] AddTodayWeight command, CancellationToken cancellationToken)
        {
            if (command.Weight <= 0 || command.Weight >= 200) return BadRequest();

            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}