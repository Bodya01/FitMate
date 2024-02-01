using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Queries.FoodRecord
{
    public record GetFoodRecordsByDate(string UserId, DateTime ConsumptionDate) : IRequest<List<FoodRecordDto>>;

    internal sealed class GetFoodRecordsByDateHanlder : IRequestHandler<GetFoodRecordsByDate, List<FoodRecordDto>>
    {
        private readonly IFoodRecordService _foodRecordService;

        public GetFoodRecordsByDateHanlder(IFoodRecordService foodRecordService)
        {
            _foodRecordService = foodRecordService;
        }

        public async Task<List<FoodRecordDto>> Handle(GetFoodRecordsByDate request, CancellationToken cancellationToken) =>
            (await _foodRecordService.GetRecordsByDate(request.ConsumptionDate, request.UserId, cancellationToken)).ToList();
    }
}