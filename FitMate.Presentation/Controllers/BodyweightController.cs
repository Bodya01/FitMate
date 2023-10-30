using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Application.Queries.BodyweightRecord;
using FitMate.Application.Queries.BodyweightTarget;
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
    public class BodyweightController : FitMateControllerBase
    {
        public BodyweightController(UserManager<FitnessUser> userManager, IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(userManager, mediator, unitOfWork, userService) { }

        public IActionResult Index() => RedirectToAction(nameof(BodyweightController.Summary));

        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var target = await _mediator.Send(new GetCurrentBodyweightTargetQuery(currentUserId), cancellationToken);
            var records = await _mediator.Send(new GetBodyweightRecordsQuery(currentUserId), cancellationToken);

            var viewModel = new BodyweightSummaryViewModel(records, target);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var target = await _mediator.Send(new GetCurrentBodyweightTargetQuery(currentUserId), cancellationToken);

            return View(target);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget(float targetWeight, DateTime targetDate, CancellationToken cancellationToken)
        {
            if (targetWeight <= 0 || targetWeight >= 200 || targetDate <= DateTime.Today) return BadRequest();

            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var newTarget = await _unitOfWork.BodyweightTargetRepository.Value.Get(e => e.UserId == currentUserId, s => s)
                .OrderByDescending(t => t.TargetDate)
                .FirstOrDefaultAsync(cancellationToken);

            newTarget ??= new BodyweightTarget() { UserId = currentUserId };

            newTarget.TargetWeight = targetWeight;
            newTarget.TargetDate = targetDate;

            if (newTarget.Id == Guid.Empty) await _unitOfWork.BodyweightTargetRepository.Value.CreateAsync(newTarget, cancellationToken);
            else await _unitOfWork.BodyweightTargetRepository.Value.UpdateAsync(newTarget, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction(nameof(BodyweightController.Summary));
        }

        [HttpGet]
        public async Task<IActionResult> EditRecords(CancellationToken cancellationToken)
        {
            var currentUserId = await _userService.GetUserIdAsync(cancellationToken);

            var records = await _mediator.Send(new GetBodyweightRecordsQuery(currentUserId), cancellationToken);

            return View(records);
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

            return RedirectToAction(nameof(BodyweightController.Summary));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight(CreateTodayWeightCommand command, CancellationToken cancellationToken)
        {
            if (command.Weight <= 0 || command.Weight >= 200) return BadRequest();

            command.UserId = await _userService.GetUserIdAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(BodyweightController.Summary));
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
    }
}