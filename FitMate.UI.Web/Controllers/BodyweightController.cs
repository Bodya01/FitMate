﻿using FitMate.DAL.Entities;
using FitMate.Data;
using FitMate.Handlers.Handlers.Bodyweight.Models.Requests;
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
            newTarget ??= new BodyweightTarget() { User = currentUser };

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
        public async Task<IActionResult> EditRecords(EditBodyweightRecordsCommand command)
        {
            if (command.RecordDates == null
                || command.RecordWeights == null
                || command.RecordDates.Length != command.RecordWeights.Length
                || command.RecordWeights.Any(x => x <= 0 || x >= 200))
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
            var currentUser = await GetUserAsync();

            var records = await _bodyweightRepository.GetBodyweightRecords(currentUser, true);

            var result = records.Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight }).ToArray();

            return Json(result);
        }
    }
}
