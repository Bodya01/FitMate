using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.UI.Web.Controllers.Base;
using FitMate.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Core.UnitOfWork;
using System.Threading;

namespace FitMate.Controllers
{
    public class BodyweightController : FitMateControllerBase
    {
        public BodyweightController(FitMateContext context,
            UserManager<FitnessUser> userManager,
            IMediator mediator,
            IUnitOfWork unitOfWork) : base(
                context,
                userManager,
                mediator,
                unitOfWork) { }

        public IActionResult Index()
        {
            return RedirectToAction("Summary");
        }

        public async Task<IActionResult> Summary()
        {
            var currentUserId = await GetUserIdAsync();

            var records = await _unitOfWork.BodyweightRecordRepository.Value.GetAllForUserAsync(currentUserId);
            var target = await _unitOfWork.BodyweightTargetRepository.Value.GetForUserAsync(currentUserId);

            var viewModel = new BodyweightSummaryViewModel(records, target);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget()
        {
            var currentUserId = await GetUserIdAsync();

            var target = await _unitOfWork.BodyweightTargetRepository.Value.GetForUserAsync(currentUserId);

            return View(target);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget(float targetWeight, DateTime targetDate, CancellationToken cancellationToken = default)
        {
            if (targetWeight <= 0 || targetWeight >= 200 || targetDate <= DateTime.Today)
            {
                return BadRequest();
            }

            var currentUserId = await GetUserIdAsync();
            var currentUser = await GetUserAsync(cancellationToken);

            var newTarget = await _unitOfWork.BodyweightTargetRepository.Value.GetForUserAsync(currentUserId);

            newTarget ??= new BodyweightTarget() { User = currentUser };

            newTarget.TargetWeight = targetWeight;
            newTarget.TargetDate = targetDate;

            if (newTarget.Id == Guid.Empty)     await _unitOfWork.BodyweightTargetRepository.Value.AddAsync(newTarget);
            else                                await _unitOfWork.BodyweightTargetRepository.Value.UpdateAsync(newTarget);

            await _unitOfWork.BodyweightTargetRepository.Value.UpdateAsync(newTarget);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> EditRecords()
        {
            var currentUserId = await GetUserIdAsync();

            var records = await _unitOfWork.BodyweightRecordRepository.Value.GetAllForUserAsync(currentUserId);

            return View(records);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm]DateTime[] rd, [FromForm]float[] rw, CancellationToken cancellationToken = default)
        {
            var command = new EditBodyweightRecordsCommand
            {
                recordWeights = rw,
                RecordDates = rd
            };

            if (command.RecordDates is null
                || command.recordWeights is null
                || command.RecordDates.Length != command.recordWeights.Length
                || command.recordWeights.Any(x => x <= 0 || x >= 200))
            {
                return BadRequest();
            }

            command.User = await GetUserAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction("Summary");
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight(AddTodayWeightCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Weight <= 0 || command.Weight >= 200)
            {
                return BadRequest();
            }

            command.User = await GetUserAsync(cancellationToken);
            await _mediator.Send(command, cancellationToken);
            
            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> GetBodyweightData(int previousDays, CancellationToken cancellationToken = default)
        {
            var currentUserId = await GetUserIdAsync(cancellationToken);

            var records = await _unitOfWork.BodyweightRecordRepository.Value.GetAllForUserAsync(currentUserId, true);

            var result = records.Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight }).ToArray();

            return Json(result);
        }
    }
}