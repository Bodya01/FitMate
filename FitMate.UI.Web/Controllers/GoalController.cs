using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using FitMate.UI.Web.Controllers.Base;
using FitMate.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitMate.Controllers
{
    [Authorize]
    public class GoalController : FitMateControllerBase
    {
        private IGoalRepository _storageRepository;

        public GoalController
            (IGoalRepository StorageService,
            FitMateContext context,
            UserManager<FitnessUser> UserManager,
            IMediator mediator)
            : base(context,
                  UserManager,
                  mediator)
        {
            _storageRepository = StorageService;
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var currentUserId = await GetUserIdAsync();

            var goals = await _storageRepository.GetAllGoals(currentUserId);

            return View(goals);
        }

        [HttpGet]
        public IActionResult AddGoal()
        {
            var model = new WeightliftingGoal() { Id = 0 };

            return View("editgoal", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditGoal(long Id)
        {
            var currentUserId = await GetUserIdAsync();

            var goal = await _storageRepository.GetGoalById(currentUserId, Id);

            if (goal is null) return BadRequest();

            return View(goal);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGoal(int Id)
        {
            var currentUserId = await GetUserIdAsync();

            var goal = await _storageRepository.GetGoalById(currentUserId, Id);

            if (goal is null) return BadRequest();

            await _storageRepository.DeleteGoalById(currentUserId, Id);

            return RedirectToAction("Summary");

        }

        [HttpPost]
        public async Task<IActionResult> EditGoal(EditGoalInputModel goalInput)
        {
            if (!TryValidateModel(goalInput)) return BadRequest();

            var currentUserId = await GetUserIdAsync();

            Goal goal = null;

            if (goalInput.Id != 0)
            {
                goal = await _storageRepository.GetGoalById(currentUserId, goalInput.Id);
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

            await _storageRepository.StoreGoal(goal);

            return RedirectToAction("Summary");
        }

        [HttpPost]
        public async Task<IActionResult> AddProgress(AddGoalProgressInputModel progress)
        {
            var currentUserId = await GetUserIdAsync();
            var currentUser = await GetUserAsync();

            var goal = await _storageRepository.GetGoalById(currentUserId, progress.GoalId);

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

            await _storageRepository.StoreGoalProgress(newProgress);

            return RedirectToAction("ViewGoal", new { ID = progress.GoalId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewGoal(long Id)
        {
            var currentUserId = await GetUserIdAsync();

            var goal = await _storageRepository.GetGoalById(currentUserId, Id);

            if (goal is null) return BadRequest();

            var progress = await _storageRepository.GetGoalProgress(currentUserId, Id);

            if (progress == null) return BadRequest();

            var viewModel = new GoalViewModel()
            {
                Goal = goal,
                Progress = progress
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress(long GoalId)
        {
            var currentUserId = await GetUserIdAsync();

            var progress = await _storageRepository.GetGoalProgress(currentUserId, GoalId, true);
            var result = Array.ConvertAll(progress, item => (WeightliftingProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight, Reps = record.Reps })
                .ToArray();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress(long GoalId)
        {
            var currentUserId = await GetUserIdAsync();

            var progress = await _storageRepository.GetGoalProgress(currentUserId, GoalId, true);

            var result = Array.ConvertAll(progress, item => (TimedProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Timespan = record.Time, Quantity = record.Quantity, QuantityUnit = record.QuantityUnit })
                .ToArray();

            return Json(result);
        }
    }
}
