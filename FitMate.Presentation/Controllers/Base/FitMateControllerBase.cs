﻿using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitMate.UI.Web.Controllers.Base
{
    [Authorize]
    public class FitMateControllerBase : Controller
    {
        protected readonly UserManager<FitnessUser> _userManager;
        protected readonly IUserService _userService;
        protected readonly IMediator _mediator;
        public IUnitOfWork _unitOfWork { get; set; }

        public FitMateControllerBase(UserManager<FitnessUser> userManager, IMediator mediator, IUnitOfWork unitOfWork, IUserService userService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
    }
}