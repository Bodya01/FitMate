using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Application.Commands.BodyweightTarget;
using FitMate.Application.Queries.BodyweightRecord;
using FitMate.Application.Queries.BodyweightTarget;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
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

            var records = await _mediator.Send(new GetBodyweightRecordsQuery(currentUserId), cancellationToken);

            records = records.OrderBy(x => x.Date).ToList();
            var result = records.Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight }).ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget(float targetWeight, DateTime targetDate, CancellationToken cancellationToken)
        {
            if (targetWeight <= 0 || targetWeight >= 200 || targetDate <= DateTime.Today) return BadRequest();

            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var command = new EditBodyweightTargetCommand(targetWeight, targetDate, currentUserId);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm] DateTime[] rd, [FromForm] float[] rw, CancellationToken cancellationToken)
        {
            var command = new EditBodyweightRecordsCommand(rd, rw, await _userService.GetUserIdAsync(cancellationToken));

            if (command.RecordDates is null
                || command.RecordWeights is null
                || command.RecordDates.Length != command.RecordWeights.Length
                || command.RecordWeights.Any(x => x <= 0 || x >= 200))
            {
                return BadRequest();
            }

            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight(CreateTodayWeightCommand command, CancellationToken cancellationToken)
        {
            if (command.Weight <= 0 || command.Weight >= 200) return BadRequest();

            command.UserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(Summary));
        }
    }
}