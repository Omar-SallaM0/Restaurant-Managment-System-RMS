using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Service;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Commands.DishesCommands.CreateDish;
public class CreateDishCommandHandler(ILogger<CreateDishCommand> logger,IRestaurantRepository repo,IRestaurantAuthorizationService restaurantAuthorizationService,
    IDishRepository dishrepo,IMapper mapper) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating A New Dish {@DishRequest}", request);
        var restaurant = await repo.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(restaurant),request.RestaurantId.ToString());
        }
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();
        var dish = mapper.Map<Dish>(request);
        await dishrepo.Create(dish);
    }
}
