using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Application.Commands.BodyweightTarget;
using FitMate.Application.Queries.BodyweightRecord;
using FitMate.Application.Queries.BodyweightTarget;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Presentation.Models.Bodyweight;
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
        public BodyweightController(IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(mediator, unitOfWork, userService) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var target = await _mediator.Send(new GetCurrentBodyweightTargetQuery(currentUserId), cancellationToken);
            var records = await _mediator.Send(new GetBodyweightRecordsQuery(currentUserId), cancellationToken);

            var viewModel = BodyweightSummaryViewModel.Create(records, target);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var target = await _mediator.Send(new GetCurrentBodyweightTargetQuery(currentUserId), cancellationToken);

            return View(target);
        }

        [HttpGet]
        public async Task<IActionResult> EditRecords(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var records = await _mediator.Send(new GetBodyweightRecordsQuery(currentUserId), cancellationToken);

            return View(records);
        }

        [HttpGet]
        public async Task<IActionResult> GetBodyweightData(int previousDays, CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var query = new GetBodyweightRecordsQuery(currentUserId, DateTime.Today, DateTime.Today.AddDays(-previousDays), false);
            var records = await _mediator.Send(query, cancellationToken);

            var result = records
                .OrderBy(x => x.Date)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight })
                .ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget(EditTargetDto input, CancellationToken cancellationToken)
        {
            if (input.TargetWeight <= 0 || input.TargetWeight >= 200 || input.TargetDate <= DateTime.Today) return BadRequest();

            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var command = new EditBodyweightTargetCommand(input.TargetWeight, input.TargetDate, currentUserId);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm] EditRecordsDto input, CancellationToken cancellationToken)
        {
            if (input.Dates is null
                || input.Weights is null
                || input.Dates.Length != input.Weights.Length
                || input.Weights.Any(x => x <= 0 || x >= 200))
            {
                return BadRequest();
            }

            var command = new EditBodyweightRecordsCommand(input.Dates, input.Weights, await _userService.GetUserIdAsync(cancellationToken));

            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight(AddTodayWeightDto input, CancellationToken cancellationToken)
        {
            if (input.Weight <= 0 || input.Weight >= 200) return BadRequest();

            var command = new CreateTodayWeightCommand(input.Weight, await _userService.GetUserIdAsync(cancellationToken));
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}