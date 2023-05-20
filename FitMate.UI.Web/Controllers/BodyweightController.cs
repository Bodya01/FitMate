using FitMate.DAL.Entities;
using FitMate.Data;
using FitMate.UI.Web.Controllers.Base;
using FitMate.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    [Authorize]
    public class BodyweightController : FitMateControllerBase
    {
        private readonly IBodyweightRepository _bodyweightRepository;

        public BodyweightController
            (IBodyweightRepository bodyweightRepository,
            FitMateContext context,
            UserManager<FitnessUser> userManager,
            IMediator mediator)
            :
            base(context,
                userManager,
                mediator)
        {
            _bodyweightRepository = bodyweightRepository;
        }

        public async Task<IActionResult> Summary()
        {
            var currentUser = await GetUserAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUser);
            var target = await _bodyweightRepository.GetBodyweightTarget(currentUser);

            var viewModel = new BodyweightSummaryViewModel(records, target);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditTarget()
        {
            var currentUser = await GetUserAsync();

            var target = await _bodyweightRepository.GetBodyweightTarget(currentUser);

            return View(target);
        }

        [HttpPost]
        public async Task<IActionResult> EditTarget(float targetWeight, DateTime targetDate)
        {
            if (targetWeight <= 0 || targetWeight >= 200 || targetDate <= DateTime.Today)
            {
                return BadRequest();
            }

            var currentUser = await GetUserAsync();

            var newTarget = await _bodyweightRepository.GetBodyweightTarget(currentUser);
            newTarget ??=  new BodyweightTarget() { User = currentUser };

            newTarget.TargetWeight = targetWeight;
            newTarget.TargetDate = targetDate;
            await _bodyweightRepository.StoreBodyweightTarget(newTarget);

            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> EditRecords()
        {
            var currentUser = await GetUserAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUser);

            return View(records);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords(DateTime[] recordDates, float[] recordWeights)
        {
            if (recordDates == null || recordWeights == null)
                return BadRequest();
            if (recordDates.Length != recordWeights.Length)
                return BadRequest();

            for (int i = 0; i < recordDates.Length; i++)
            {
                if (recordWeights[i] <= 0 || recordWeights[i] >= 200)
                    return BadRequest();
            }

            var currentUser = await GetUserAsync();

            await _bodyweightRepository.DeleteExistingRecords(currentUser);

            var records = new List<BodyweightRecord>();
            for (int i = 0; i < recordDates.Length; i++)
            {
                var newRecord = new BodyweightRecord()
                {
                    User = currentUser,
                    Date = recordDates[i],
                    Weight = recordWeights[i]
                };
                records.Add(newRecord);
            }

            await _bodyweightRepository.StoreBodyweightRecords(records);
            return RedirectToAction("Summary");
        }

        [HttpPost]
        public async Task<IActionResult> AddTodayWeight(float weight)
        {
            if (weight <= 0 || weight >= 200)
                return BadRequest();

            var currentUser = await GetUserAsync();

            BodyweightRecord newRecord = new BodyweightRecord()
            {
                User = currentUser,
                Date = DateTime.Today,
                Weight = weight
            };

            await _bodyweightRepository.StoreBodyweightRecord(newRecord);
            return RedirectToAction("Summary");
        }

        [HttpGet]
        public async Task<IActionResult> GetBodyweightData(int previousDays)
        {
            var currentUser = await GetUserAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUser, true);

            var result = records.Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight }).ToArray();

            return Json(result);
        }
    }
}
