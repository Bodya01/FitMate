using FitMate.Core.Context;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public NutritionController(FitMateContext context, UserManager<FitnessUser> userManager, IMediator mediator, IUnitOfWork unitOfWork) : base(context, userManager, mediator, unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> AddFood(DateTime date, CancellationToken cancellationToken)
        {
            if (date.Ticks == 0) date = DateTime.Today;
            ViewData["selectedDate"] = date;

            var currentUserId = await GetUserIdAsync();

            var model = new NewFoodModel()
            {
                FoodRecords = await _context.FoodRecords.Where(r => r.UserId == currentUserId && r.ConsumptionDate == date).ToListAsync(cancellationToken),
                UserFoods = await _unitOfWork.FoodRepository.Value.Get(e => true, s => s).ToListAsync(cancellationToken),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFood(Food food, CancellationToken cancellationToken)
        {
            if (food.Id == Guid.Empty) await _unitOfWork.FoodRepository.Value.CreateAsync(food, cancellationToken);
            else await _unitOfWork.FoodRepository.Value.UpdateAsync(food, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction("AddFood");
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords(DateTime date, Guid[] foodIds, float[] quantities, CancellationToken cancellationToken)
        {
            if (foodIds.Length != quantities.Length || foodIds.Length == 0) return BadRequest();

            var currentUser = await GetUserAsync(cancellationToken);

            var existingRecords = await _unitOfWork.FoodRecordRepository.Value.Get(e => e.UserId == currentUser.Id && e.ConsumptionDate == date, s => s)
                .ToListAsync(cancellationToken);
            await _unitOfWork.FoodRecordRepository.Value.DeleteRangeAsync(existingRecords);

            var newRecords = new FoodRecord[foodIds.Length];
            for (int i = 0; i < foodIds.Length; i++)
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

            return RedirectToAction("AddFood");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFood(Guid id, CancellationToken cancellationToken)
        {
            var targetFood = await _unitOfWork.FoodRepository.Value.GetByIdAsync(id, cancellationToken);
            if (targetFood is null) return BadRequest();

            await _unitOfWork.FoodRepository.Value.DeleteAsync(targetFood, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return RedirectToAction("AddFood");
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

            var userTarget = await _context.NutritionTargets.FirstOrDefaultAsync(record => record.UserId == currentUserId);
            userTarget ??= new NutritionTarget();

            var summaryModel = new NutritionSummaryModel()
            {
                Records = userRecords,
                Target = userTarget
            };

            return View(summaryModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetNutritionData(uint previousDays = 7)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var records = await _context.FoodRecords
                .Where(record => record.ConsumptionDate >= DateTime.Today.AddDays(-previousDays) && record.User == currentUser)
                .Include(record => record.Food)
                .ToArrayAsync();

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