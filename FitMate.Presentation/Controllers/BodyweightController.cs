using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Application.Commands.BodyweightTarget;
using FitMate.Application.Queries.BodyweightRecord;
using FitMate.Application.Queries.BodyweightTarget;
using FitMate.Business.Interfaces;
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
    public sealed class BodyweightController : FitMateControllerBase
    {
        public BodyweightController(IMediator mediator, IUserService userService)
            : base(mediator, userService) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var target = await _mediator.Send(new GetCurrentBodyweightTarget(currentUserId), cancellationToken);
            var records = await _mediator.Send(new GetBodyweightRecords(currentUserId), cancellationToken);

            var viewModel = BodyweightSummaryViewModel.Create(records, target);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var target = await _mediator.Send(new GetCurrentBodyweightTarget(currentUserId), cancellationToken);

            return View(target);
        }

        [HttpGet]
        public async Task<IActionResult> EditRecords(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var records = await _mediator.Send(new GetBodyweightRecords(currentUserId), cancellationToken);

            return View(records);
        }

        [HttpGet]
        public async Task<IActionResult> GetBodyweightData(int previousDays, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var query = new GetBodyweightRecords(currentUserId, DateTime.Today, DateTime.Today.AddDays(-previousDays), false);
            var records = await _mediator.Send(query, cancellationToken);

            var result = records
                .OrderBy(x => x.Date)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight })
                .ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget([FromBody] EditBodyweightTarget command, CancellationToken cancellationToken)
        {
            if (command.Weight <= 0 || command.Weight >= 200 || command.Date <= DateTime.Today) return BadRequest();

            command.UserId = await _userService.GetUserIdAsync(cancellationToken);

            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm] EditBodyweightRecords command, CancellationToken cancellationToken)
        {
            if (command.Dates is null
                || command.Weights is null
                || command.Dates.Length != command.Weights.Length
                || command.Weights.Any(x => x <= 0 || x >= 200))
            {
                return BadRequest();
            }

            command.UserId = await _userService.GetUserIdAsync(cancellationToken);

            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight([FromBody] CreateTodayWeight command, CancellationToken cancellationToken)
        {
            if (command.Weight <= 0 || command.Weight >= 200) return BadRequest();

            command.UserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}