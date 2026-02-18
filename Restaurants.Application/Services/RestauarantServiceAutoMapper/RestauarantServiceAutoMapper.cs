using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Restaurants;
using Resturants.Domain.Entities;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Services.RestauarantServiceAutoMapper;
public class RestauarantServiceAutoMapper(ILogger<RestauarantServiceAutoMapper> logger, IRestaurantRepository restaurantRepository, IMapper mapper) : IRestauarantServiceAutoMapper
{
    public async Task<int> Create(RestaurantDto dto)
    {
        logger.LogInformation("Creating A New Restaurant ");
        var res = mapper.Map<Restaurant>(dto);
        var id = await restaurantRepository.Create(res);
        return id;
    }
    public async Task<IEnumerable<RestaurantDto?>> GetAllRestaurants()
    {
        logger.LogInformation("Getting All Restaurants");

        var restaurants = await restaurantRepository.GetAllResturantsAsync();
        var RestaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return RestaurantsDto;
    }

    public async Task<RestaurantDto> GetRestaurant(int id)
    {
        logger.LogInformation($"Getting Restaurant {id}");

        var res = await restaurantRepository.GetRestaurantByIdAsync(id);
        var resDto = mapper.Map<RestaurantDto>(res);
        return resDto;
    }
}
