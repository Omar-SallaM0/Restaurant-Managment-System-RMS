using Microsoft.Extensions.Logging;
using Resturants.Domain.Entities;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Services.RestaurantService;
public class RestaurantService(IRestaurantRepository repo, ILogger<RestaurantService> logger) :IRestaurantService
{
    public async Task<IEnumerable<Restaurant>> GetAResturants()
    {
        logger.LogInformation("Get All Restaurants");
        var res = await repo.GetAllResturantsAsync();
        return res;
    }

    public async Task<Restaurant> GetRestaurantById(int id)
    {
        logger.LogInformation("Get Restaurant {id}",id);
        var res = await repo.GetRestaurantByIdAsync(id);
        return res;
    }
    public async Task DeleteRestaurantById(int id)
    {
        logger.LogInformation("Get Restaurant {id}", id);
        var res = await repo.GetRestaurantByIdAsync(id);
        await repo.Delete(res);
    }

}
