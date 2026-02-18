using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Restaurants;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Services.RestaurantDtoService;
public class RestaurantDtoService(IRestaurantRepository restaurantRepository, ILogger<RestaurantDtoService> logger) : IRestaurantDtoService
{
    public async Task<IEnumerable<RestaurantDto?>> GetAllRestaurant()
    {
        logger.LogInformation("Get All Restaurants");
        var res = await restaurantRepository.GetAllResturantsAsync();
        var restaurantsDto = res.Select(RestaurantDto.FromEntity).ToList();
        return restaurantsDto;
    }

    public async Task<RestaurantDto> GetAllRestaurantByID(int id)
    {
        logger.LogInformation("Get Restaurant By Id : {0}",id);
        var restaurant = await restaurantRepository.GetRestaurantByIdAsync(id);
        var res = RestaurantDto.FromEntity(restaurant);
        return res;
    }
}
