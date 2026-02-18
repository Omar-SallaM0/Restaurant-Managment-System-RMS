using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Commands.RestaurantCommands.CreateRestaurant;
public class RestaurantCommandHandler(ILogger<RestaurantCommandHandler> logger,IRestaurantRepository repo, IMapper mapper,IUserContext userContext) : IRequestHandler<RestaurantCommand, int>
{
    public async Task<int> Handle(RestaurantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = userContext.GetCurrentUser()
                ?? throw new UnAuthorizedException();

            var currentUserId = currentUser.Id;
            logger.LogInformation("User with id: {userId} is creating a new restaurant with name: {restaurantName}", currentUserId, request.Name);

            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUserId;
            int id = await repo.Create(restaurant);
            logger.LogInformation("restaurant with id: {restaurantId} , name: {restaurantName} created successfully.", id, request.Name);
            return id;
        }
        catch (UnAuthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create restaurant due to exception");
            throw;
        }
    }
}
