using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Resturants.Domain.Entities;
using Resturants.Domain.Enums;

namespace Restaurants.Infrastructure.Authorization.Service;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext usercontext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var user = usercontext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
            user.Email,
            operation,
            restaurant.Name);

        if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (operation == ResourceOperation.Delete && user.IsinRole(UserRoles.Admin.ToString()))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((operation == ResourceOperation.Delete || operation == ResourceOperation.Update)
            && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        return false;

    }
}
