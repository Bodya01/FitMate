using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.UI.Web.Controllers.Base;
using FitMate.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using FitMate.Applcation.Commands.Bodyweight;
using FitMate.Core.Repositories.Interfaces;

namespace FitMate.Controllers
{
    [Authorize]
    public class BodyweightController : FitMateControllerBase
    {
        private readonly IBodyweightRepository _bodyweightRepository;

        public BodyweightController(
            IBodyweightRepository bodyweightRepository,
            FitMateContext context,
            UserManager<FitnessUser> userManager,
            IMediator mediator
            ) : base(
                context,
                userManager,
                mediator
                )
        {
            _bodyweightRepository = bodyweightRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Summary");
        }

        public async Task<IActionResult> Summary()
        {
            var currentUserId = await GetUserIdAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUserId);
            var target = await _bodyweightRepository.GetBodyweightTarget(currentUserId);

            var viewModel = new BodyweightSummaryViewModel(records, target);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget()
        {
            var currentUserId = await GetUserIdAsync();

            var target = await _bodyweightRepository.GetBodyweightTarget(currentUserId);

            return View(target);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget(float targetWeight, DateTime targetDate)
        {
            if (targetWeight <= 0 || targetWeight >= 200 || targetDate <= DateTime.Today)
            {
                return BadRequest();
            }

            var currentUserId = await GetUserIdAsync();
            var currentUser = await GetUserAsync();

            var newTarget = await _bodyweightRepository.GetBodyweightTarget(currentUserId);
            newTarget ??= new BodyweightTarget() { User = currentUser };

            newTarget.TargetWeight = targetWeight;
            newTarget.TargetDate = targetDate;
            await _bodyweightRepository.StoreBodyweightTarget(newTarget);

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> EditRecords()
        {
            var currentUserId = await GetUserIdAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUserId);

            return View(records);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm]DateTime[] rd, [FromForm]float[] rw)
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

            command.User = await GetUserAsync();
            await _mediator.Send(command);

            return RedirectToAction("Summary");
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight(AddTodayWeightCommand command)
        {
            if (command.Weight <= 0 || command.Weight >= 200)
            {
                return BadRequest();
            }

            command.User = await GetUserAsync();
            await _mediator.Send(command);
            
            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> GetBodyweightData(int previousDays)
        {
            var currentUserId = await GetUserIdAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUserId, true);

            var result = records.Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight }).ToArray();

            return Json(result);
        }
    }
}
