using FitMate.Core.UnitOfWork;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using FitMate.UI.Web.Controllers.Base;
using FitMate.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    public class GoalController : FitMateControllerBase
    {

        public GoalController
            (FitMateContext context,
            UserManager<FitnessUser> UserManager,
            IMediator mediator, IUnitOfWork unitOfWork)
            : base(context,
                  UserManager,
                  mediator,
                  unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var currentUserId = await GetUserIdAsync();

            var goals = await _unitOfWork.GoalRepository.Value.GetAllForUserAsync(currentUserId);

            return View(goals);
        }

        [HttpGet]
        public IActionResult AddGoal()
        {
            var model = new WeightliftingGoal { Id = Guid.Empty };

            return View("editgoal", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditGoal(Guid Id)
        {
            var currentUserId = await GetUserIdAsync();

            var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(currentUserId, Id);

            if (goal is null) return BadRequest();

            return View(goal);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGoal(Guid Id)
        {
            var currentUserId = await GetUserIdAsync();

            var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(currentUserId, Id);

            if (goal is null) return BadRequest();

            await _unitOfWork.GoalRepository.Value.DeleteAsync(currentUserId, Id);

            return RedirectToAction("Summary");

        }

        [HttpPost]
        public async Task<IActionResult> EditGoal(EditGoalInputModel goalInput)
        {
            if (!TryValidateModel(goalInput)) return BadRequest();

            var currentUserId = await GetUserIdAsync();

            Goal goal = null;

            if (goalInput.Id != Guid.Empty)
            {
                goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(currentUserId, goalInput.Id);
            }
            else
            {
                switch (goalInput.Type.ToLower())
                {
                    case "weightlifting":
                        goal = new WeightliftingGoal();
                        break;
                    case "timed":
                        goal = new TimedGoal();
                        break;
                }
            }

            switch (goal)
            {
                case WeightliftingGoal wGoal:
                    wGoal.Reps = goalInput.Reps;
                    wGoal.Weight = goalInput.Weight;
                    break;
                case TimedGoal tGoal:
                    tGoal.Quantity = (int)goalInput.Quantity;
                    tGoal.QuantityUnit = goalInput.QuantityUnit;
                    tGoal.Time = new TimeSpan(goalInput.Hours, goalInput.Minutes, goalInput.Seconds);
                    break;
            }

            goal.Activity = goalInput.Activity;
            goal.User = await GetUserAsync();

            if (goal.Id == Guid.Empty)
            {
                await _unitOfWork.GoalRepository.Value.AddAsync(goal);
            }
            else
            {
                await _unitOfWork.GoalRepository.Value.UpdateAsync(goal);
            }

            return RedirectToAction("Summary");
        }

        [HttpPost]
        public async Task<IActionResult> AddProgress(AddGoalProgressInputModel progress)
        {
            var currentUserId = await GetUserIdAsync();
            var currentUser = await GetUserAsync();

            var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(currentUserId, progress.GoalId);

            if (goal is null) return BadRequest();

            GoalProgress newProgress = null;

            switch (progress.Type.ToLower())
            {
                case "weightlifting":
                    newProgress = new WeightliftingProgress()
                    {
                        Weight = progress.Weight,
                        Reps = progress.Reps
                    };
                    break;
                case "timed":
                    newProgress = new TimedProgress()
                    {
                        Time = new TimeSpan(progress.Hours, progress.Minutes, progress.Seconds),
                        Quantity = progress.Quantity
                    };
                    break;
                default:
                    return BadRequest();
            }

            newProgress.Goal = goal;
            newProgress.Date = progress.Date;
            newProgress.User = currentUser;

            await _unitOfWork.GoalProgressRepository.Value.AddAsync(newProgress);

            return RedirectToAction("ViewGoal", new { ID = progress.GoalId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewGoal(Guid Id)
        {
            var currentUserId = await GetUserIdAsync();

            var goal = await _unitOfWork.GoalRepository.Value.GetByIdAsync(currentUserId, Id);

            if (goal is null) return BadRequest();

            var progress = await _unitOfWork.GoalProgressRepository.Value.GetForUserAsync(currentUserId, Id);

            if (progress == null) return BadRequest();

            var viewModel = new GoalViewModel()
            {
                Goal = goal,
                Progress = progress
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress(Guid goalId)
        {
            var currentUserId = await GetUserIdAsync();

            var progress = await _unitOfWork.GoalProgressRepository.Value.GetForUserAsync(currentUserId, goalId, true);
            var result = Array.ConvertAll(progress.ToArray(), item => (WeightliftingProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight, Reps = record.Reps })
                .ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress(Guid goalId)
        {
            var currentUserId = await GetUserIdAsync();

            var progress = await _unitOfWork.GoalProgressRepository.Value.GetForUserAsync(currentUserId, goalId, true);

            var result = Array.ConvertAll(progress.ToArray(), item => (TimedProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Timespan = record.Time, Quantity = record.Quantity, QuantityUnit = record.QuantityUnit })
                .ToList();

            return Json(result);
        }
    }
}
