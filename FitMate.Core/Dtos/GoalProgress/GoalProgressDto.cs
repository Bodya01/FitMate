﻿using FitMate.Infrastucture.Dtos.Base;
using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Infrastucture.Dtos.GoalProgress;

public record GoalProgressDto(Guid Id, DateTime Date, UserDto User, GoalDto Goal) : DtoBase;