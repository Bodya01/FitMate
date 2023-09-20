using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FitMate.Core.UnitOfWork;

namespace FitMate.Controllers
{
    public class NutritionController : FitMateControllerBase
    {
        public NutritionController(FitMateContext context,
            UserManager<FitnessUser> userManager,
            IMediator mediator,
            IUnitOfWork unitOfWork)
            : base(context,
                  userManager,
                  mediator,
                  unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> AddFood(DateTime date)
        {
            if (date.Ticks == 0) date = DateTime.Today;
            ViewData["selectedDate"] = date;

            var currentUserId = await GetUserIdAsync();

            var model = new NewFoodModel()
            {
                FoodRecords = await _context.FoodRecords.Where(r => r.UserId == currentUserId && r.ConsumptionDate == date).ToListAsync(),
                UserFoods = await _context.Foods.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFood(Food food)
        {
            if (food.Id == Guid.Empty)  _context.Foods.Add(food);
            else _context.Foods.Update(food);

            await _context.SaveChangesAsync();

            return RedirectToAction("AddFood");
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords(DateTime date, Guid[] foodIDs, float[] quantities)
        {
            if (foodIDs.Length != quantities.Length || foodIDs.Length == 0) return BadRequest();

            var currentUser = await GetUserAsync();

            var existingRecords = await _context.FoodRecords.Where(record => record.User == currentUser && record.ConsumptionDate == date).ToListAsync();
            _context.FoodRecords.RemoveRange(existingRecords);

            var newRecords = new FoodRecord[foodIDs.Length];
            for (int i = 0; i < foodIDs.Length; i++)
            {
                newRecords[i] = new FoodRecord()
                {
                    ConsumptionDate = date,
                    User = currentUser,
                    FoodId = foodIDs[i],
                    Quantity = quantities[i]
                };
            }
            _context.FoodRecords.AddRange(newRecords);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddFood");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFood(Guid Id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var targetFood = await _context.Foods.FirstOrDefaultAsync(food => food.Id == Id);
            if (targetFood is null) return BadRequest();

            _context.Foods.Remove(targetFood);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddFood");
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var currentUser = await GetUserAsync();

            var userRecords = await _context.FoodRecords
                .Where(record => record.User == currentUser && record.ConsumptionDate >= DateTime.Today.AddDays(-28))
                .Include(record => record.Food)
                .ToListAsync();

            var userTarget = await _context.NutritionTargets.FirstOrDefaultAsync(record => record.User == currentUser);
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
