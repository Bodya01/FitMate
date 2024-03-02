using FitMate.Application.Commands.Food;
using FitMate.Application.Commands.FoodRecord;
using FitMate.Application.Queries.Food;
using FitMate.Application.Queries.FoodRecord;
using FitMate.Application.Queries.Nutrition;
using FitMate.Infrastructure.Extensions;
using FitMate.Presentation.ViewModels.Nutrition;
using FitMate.UI.Web.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    //TODO: Implement single handlers for each endpoint, move all logic to handlers, implement validators
    public sealed class NutritionController : FitMateControllerBase
    {
        public NutritionController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public IActionResult Index() => RedirectToAction(nameof(Summary));

        [HttpGet]
        public async Task<IActionResult> Summary(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNutritionSummary(_currentUserId), cancellationToken);
            return View(NutritionSummaryViewModel.Create(response.Records, response.Target));
        }

        [HttpGet]
        public async Task<IActionResult> GetNutritionData(CancellationToken cancellationToken, uint previousDays = 7)
        {
            var records = await _mediator.Send(new GetFoodRecords(_currentUserId, previousDays), cancellationToken);

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

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddFood(DateTime date, CancellationToken cancellationToken)
        {
            if (date.Ticks == default) date = DateTime.Today;

            var model = new NewFoodViewModel()
            {
                SelectedDate = date,
                Foods = await _mediator.Send(new GetAllFoods(), cancellationToken),
                FoodRecords = await _mediator.Send(new GetFoodRecordsByDate(_currentUserId, date), cancellationToken)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewFood([FromForm] CreateFood command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return RedirectToAction(nameof(AddFood));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFood([FromForm] UpdateFood command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return RedirectToAction(nameof(AddFood));
        }

        [HttpPost]
        public async Task<IActionResult> EditRecords([FromForm] EditFoodRecords command, CancellationToken cancellationToken)
        {
            if (!command.FoodIds.IsNullOrEmpty() && !command.Quantities.IsNullOrEmpty() && command.FoodIds.Count != command.Quantities.Count) return BadRequest();

            command.UserId = _currentUserId;
            await _mediator.Send(command, cancellationToken);

            return RedirectToAction(nameof(AddFood));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFood([FromForm] DeleteFood command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return RedirectToAction(nameof(AddFood));
        }
    }
}