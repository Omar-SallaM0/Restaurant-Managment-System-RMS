using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Dish;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Queries.Dish;
public class GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger,IRestaurantRepository repo,IMapper mapper) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching dish with id {DishId} for restaurant {RestaurantId}", request.DishId, request.RestaurantId);
        var res = await repo.GetRestaurantByIdAsync(request.RestaurantId);
        if (res == null)
        {
            throw new NotFoundException(nameof(res),request.RestaurantId.ToString());
        }
        var dish = mapper.Map<DishDto>(res.Dishes.FirstOrDefault(d=>d.id==request.DishId));
        return dish;
    }
}
