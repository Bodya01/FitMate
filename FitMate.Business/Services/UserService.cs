using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastucture.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FitMate.Business.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly UserManager<FitnessUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(UserManager<FitnessUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) throw new EntityNotFoundException($"User with id {userId} does not exist");

            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserHeight(string userId, int height, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) throw new EntityNotFoundException($"User with id {userId} does not exist");

            user.Height = height;

            await _unitOfWork.UserRepository.Value.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}