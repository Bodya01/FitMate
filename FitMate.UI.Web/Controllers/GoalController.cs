﻿using FitMate.DAL.Entities;
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

namespace FitMate.Controllers
{
    [Authorize]
    public class GoalController : FitMateControllerBase
    {
        private IGoalRepository storageService;

        public GoalController
            (IGoalRepository StorageService,
            FitMateContext context,
            UserManager<FitnessUser> UserManager,
            IMediator mediator)
            : base(context,
                  UserManager,
                  mediator)
        {
            this.storageService = StorageService;
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var currentUser = await GetUserAsync();

            var goals = await storageService.GetAllGoals(currentUser);

            return View(goals);
        }

        [HttpGet]
        public async Task<IActionResult> AddGoal()
        {
            WeightliftingGoal model = new WeightliftingGoal()
            {
                Id = 0
            };

            return View("editgoal", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditGoal(long ID)
        {
            var currentUser = await GetUserAsync();

            var goal = await storageService.GetGoalByID(currentUser, ID);
            if (goal == null)
                return BadRequest();

            return View(goal);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGoal(int ID)
        {
            var currentUser = await GetUserAsync();

            var goal = await storageService.GetGoalByID(currentUser, ID);
            if (goal == null)
                return BadRequest();

            await storageService.DeleteGoalByID(currentUser, ID);

            return RedirectToAction("Summary");

        }

        [HttpPost]
        public async Task<IActionResult> EditGoal(EditGoalInputModel GoalInput)
        {
            if (TryValidateModel(GoalInput) == false)
                return BadRequest();

            var currentUser = await GetUserAsync();

            Goal goal = null;

            if (GoalInput.ID != 0)
            {
                goal = await storageService.GetGoalByID(currentUser, GoalInput.ID);
            }
            else
            {
                switch (GoalInput.Type.ToLower())
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
                    wGoal.Reps = GoalInput.Reps;
                    wGoal.Weight = GoalInput.Weight;
                    break;
                case TimedGoal tGoal:
                    tGoal.Quantity = (int)GoalInput.Quantity;
                    tGoal.QuantityUnit = GoalInput.QuantityUnit;
                    tGoal.Time = new TimeSpan(GoalInput.Hours, GoalInput.Minutes, GoalInput.Seconds);
                    break;
            }

            goal.Activity = GoalInput.Activity;
            goal.User = currentUser;

            await storageService.StoreGoal(goal);

            return RedirectToAction("Summary");
        }

        [HttpPost]
        public async Task<IActionResult> AddProgress(AddGoalProgressInputModel Progress)
        {
            var currentUser = await GetUserAsync();

            var goal = await storageService.GetGoalByID(currentUser, Progress.GoalID);

            if (goal == null)
                return BadRequest();

            GoalProgress newProgress = null;

            switch (Progress.Type.ToLower())
            {
                case "weightlifting":
                    newProgress = new WeightliftingProgress()
                    {
                        Weight = Progress.Weight,
                        Reps = Progress.Reps
                    };
                    break;
                case "timed":
                    newProgress = new TimedProgress()
                    {
                        Time = new TimeSpan(Progress.Hours, Progress.Minutes, Progress.Seconds),
                        Quantity = Progress.Quantity
                    };
                    break;
                default:
                    return BadRequest();
            }

            newProgress.Goal = goal;
            newProgress.Date = Progress.Date;
            newProgress.User = currentUser;

            await storageService.StoreGoalProgress(newProgress);

            return RedirectToAction("ViewGoal", new { ID = Progress.GoalID });
        }

        [HttpGet]
        public async Task<IActionResult> ViewGoal(long ID)
        {
            var currentUser = await GetUserAsync();

            var goal = await storageService.GetGoalByID(currentUser, ID);
            if (goal == null)
                return BadRequest();

            var progress = await storageService.GetGoalProgress(currentUser, ID);

            if (progress == null)
                return BadRequest();

            var viewModel = new GoalViewModel()
            {
                Goal = goal,
                Progress = progress
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightliftingProgress(long GoalID)
        {
            var currentUser = await GetUserAsync();

            var progress = await storageService.GetGoalProgress(currentUser, GoalID, true);
            var result = Array.ConvertAll(progress, item => (WeightliftingProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Weight = record.Weight, Reps = record.Reps })
                .ToArray();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTimedProgress(long GoalID)
        {
            var currentUser = await GetUserAsync();

            var progress = await storageService.GetGoalProgress(currentUser, GoalID, true);

            var result = Array.ConvertAll(progress, item => (TimedProgress)item)
                .Select(record => new { Date = record.Date.ToString("d"), Timespan = record.Time, Quantity = record.Quantity, QuantityUnit = record.QuantityUnit })
                .ToArray();

            return Json(result);
        }
    }
}
