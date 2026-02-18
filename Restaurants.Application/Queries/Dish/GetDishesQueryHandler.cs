using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Dish;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Queries.Dish;
public class GetDishesQueryHandler(ILogger<GetDishesQueryHandler> logger,IMapper mapper,IRestaurantRepository repo) : IRequestHandler<GetDishesQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all dishes for restaurant with ID {RestaurantId}", request.RestaurantId);
        var res = await repo.GetRestaurantByIdAsync(request.RestaurantId);
        if (res == null)
        {
            throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
        }
        var dishes = mapper.Map<List<DishDto>>(res.Dishes);
        return dishes;
    }
}
