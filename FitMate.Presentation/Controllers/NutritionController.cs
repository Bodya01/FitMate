using FitMate.Application.Commands.Food;
using FitMate.Application.Queries.Food;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    public class NutritionController : FitMateControllerBase
    {
        public NutritionController(UserManager<FitnessUser> userManager, IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
            : base(userManager, mediator, unitOfWork, userService) { }

        [HttpGet]
        public async Task<IActionResult> AddFood(DateTime date, CancellationToken cancellationToken)
        {
            if (date.Ticks == default) date = DateTime.Today;
            ViewData["selectedDate"] = date;

            var currentUserId = await GetUserIdAsync(cancellationToken);

            var model = new NewFoodModel()
            {
                FoodRecords = await _unitOfWork.FoodRecordRepository.Value.Get(e => e.UserId == currentUserId && e.ConsumptionDate == date, s => s).ToListAsync(cancellationToken),
                UserFoods = await _unitOfWork.FoodRepository.Value.Get(e => true, s => s).ToListAsync(cancellationToken),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFood(FoodDto food, CancellationToken cancellationToken)
        {
            var command = new CreateFoodCommand(food);

            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(AddFood));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords(DateTime date, Guid[] foodIds, float[] quantities, CancellationToken cancellationToken)
        {
            if (foodIds.Length != quantities.Length || !foodIds.Any()) return BadRequest();

            var currentUser = await GetUserAsync(cancellationToken);

            var existingRecords = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.UserId == currentUser.Id && e.ConsumptionDate == date, s => s)
                .ToListAsync(cancellationToken);
            await _unitOfWork.FoodRecordRepository.Value.DeleteRangeAsync(existingRecords, cancellationToken);

            var newRecords = new FoodRecord[foodIds.Length];
            for (var i = 0; i < foodIds.Length; i++)
            {
                newRecords[i] = new FoodRecord()
                {
                    ConsumptionDate = date,
                    User = currentUser,
                    FoodId = foodIds[i],
                    Quantity = quantities[i]
                };
            }
            await _unitOfWork.FoodRecordRepository.Value.CreateRangeAsync(newRecords, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction(nameof(AddFood));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFood(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new GetFoodQuery(id), cancellationToken);
            return RedirectToAction(nameof(AddFood));
        }

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var currentUserId = await GetUserIdAsync(cancellationToken);

            var userRecordsQuery = _unitOfWork.FoodRecordRepository.Value.Get(e => e.UserId == currentUserId && e.ConsumptionDate >= DateTime.Today.AddDays(-28), s => s);

            foreach (var record in userRecordsQuery)
            {
                await _unitOfWork.FoodRecordRepository.Value.LoadNavigationPropertyExplicitly(record, r => r.Food, cancellationToken);
            }

            var userRecords = await userRecordsQuery.ToListAsync(cancellationToken);

            var userTarget = await _unitOfWork.NutritionTargetRepository.Value.GetTargetForUserAsync(currentUserId, cancellationToken);
            userTarget ??= new NutritionTarget();

            var summaryModel = new NutritionSummaryModel()
            {
                Records = userRecords,
                Target = userTarget
            };

            return View(summaryModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetNutritionData(CancellationToken cancellationToken, uint previousDays = 7)
        {
            var currentUserId = await GetUserIdAsync();

            var records = await _unitOfWork.FoodRecordRepository.Value
                .Get(e => e.ConsumptionDate >= DateTime.Today.AddDays(-previousDays) && e.UserId == currentUserId, s => s)
                .ToListAsync(cancellationToken);

            foreach (var record in records)
            {
                await _unitOfWork.FoodRecordRepository.Value.LoadNavigationPropertyExplicitly(record, s => s.Food, cancellationToken);
            }

            var result = records
                .GroupBy(record => record.ConsumptionDate)
                .Select(grouping =>
                new
                {
                    Date = grouping.Key.ToString("d"),
                    Calories = grouping.Sum(r => r.Food.Calories),
                    Carbs = grouping.Sum(r => r.Food.Carbohydrates),
                    Protein = grouping.Sum(r => r.Food.Protein),
                    Fat = grouping.Sum(r => r.Food.Fat)
                })
                .ToList();

            return Json(result);
        }
    }

    public class NewFoodModel
    {
        public List<Food> UserFoods { get; set; }
        public List<FoodRecord> FoodRecords { get; set; }
    }
    public class NutritionSummaryModel
    {
        public List<FoodRecord> Records { get; set; }
        public NutritionTarget Target { get; set; }
    }
}