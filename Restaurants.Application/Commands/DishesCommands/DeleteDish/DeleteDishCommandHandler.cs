using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Service;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Commands.DishesCommands.DeleteDish;
internal class DeleteDishCommandHandler(IRestaurantRepository repo,IRestaurantAuthorizationService restaurantAuthorizationService
    ,IDishRepository dishRepo,ILogger<DeleteDishCommandHandler> logger) : IRequestHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Dsih {0} from Restaurant :{1}", request.dishId, request.RestaurantId);
        var res=await repo.GetRestaurantByIdAsync(request.RestaurantId);
        if (res == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }
        if (!restaurantAuthorizationService.Authorize(res, ResourceOperation.Update))
            throw new ForbidException();
        await dishRepo.DeleteAllDishes(res.Dishes);
    }
}
