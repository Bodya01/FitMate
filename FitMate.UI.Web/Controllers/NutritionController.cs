using FitMate.DAL.Entities;
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

namespace FitMate.Controllers
{

    [Authorize]
    public class NutritionController : FitMateControllerBase
    {

        public NutritionController
            (FitMateContext context,
            UserManager<FitnessUser> userManager,
            IMediator mediator)
            : base(context,
                  userManager,
                  mediator)
        {

        }

        [HttpGet]
        public async Task<IActionResult> AddFood(DateTime date)
        {
            if (date.Ticks == 0)
                date = DateTime.Today;
            ViewData["selectedDate"] = date;

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var model = new NewFoodModel()
            {
                FoodRecords = await _context.FoodRecords.Where(record => record.User == currentUser && record.ConsumptionDate == date).ToArrayAsync(),
                UserFoods = await _context.UserFoods.Where(record => record.CreatedBy == currentUser).ToArrayAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFood(Food food)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            food.CreatedBy = currentUser;

            if (food.Id == 0)
                _context.UserFoods.Add(food);
            else
                _context.UserFoods.Update(food);

            await _context.SaveChangesAsync();

            return RedirectToAction("AddFood");
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords(DateTime Date, long[] FoodIDs, float[] Quantities)
        {
            if (FoodIDs.Length != Quantities.Length || FoodIDs.Length == 0)
                return BadRequest();

            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var existingRecords = await _context.FoodRecords.Where(record => record.User == currentUser && record.ConsumptionDate == Date).ToArrayAsync();
            _context.FoodRecords.RemoveRange(existingRecords);

            var newRecords = new FoodRecord[FoodIDs.Length];
            for (int i = 0; i < FoodIDs.Length; i++)
            {
                newRecords[i] = new FoodRecord()
                {
                    ConsumptionDate = Date,
                    User = currentUser,
                    FoodID = FoodIDs[i],
                    Quantity = Quantities[i]
                };
            }
            _context.FoodRecords.AddRange(newRecords);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddFood");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFood(long ID)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var targetFood = await _context.UserFoods.FirstOrDefaultAsync(food => food.Id == ID);
            if (targetFood == null || targetFood.CreatedBy != currentUser)
                return BadRequest();

            _context.UserFoods.Remove(targetFood);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddFood");
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var userRecords = await _context.FoodRecords
                .Where(record => record.User == currentUser && record.ConsumptionDate >= DateTime.Today.AddDays(-28))
                .Include(record => record.Food)
                .ToArrayAsync();
            var userTarget = await _context.NutritionTargets.FirstOrDefaultAsync(record => record.User == currentUser);
            if (userTarget == null)
                userTarget = new NutritionTarget();

            var summaryModel = new NutritionSummaryModel()
            {
                Records = userRecords,
                Target = userTarget
            };

            return View(summaryModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetNutritionData(uint PreviousDays = 7)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var records = await _context.FoodRecords
                .Where(record => record.ConsumptionDate >= DateTime.Today.AddDays(-PreviousDays) && record.User == currentUser)
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
            .ToArray();

            return Json(result);
        }
    }


    public class NewFoodModel
    {
        public Food[] UserFoods { get; set; }
        public FoodRecord[] FoodRecords { get; set; }
    }
    public class NutritionSummaryModel
    {
        public FoodRecord[] Records { get; set; }
        public NutritionTarget Target { get; set; }
    }
}
