using Restaurants.Application.DTO.Restaurants;

namespace Restaurants.Application.Services.RestauarantServiceAutoMapper;
public interface IRestauarantServiceAutoMapper
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto> GetRestaurant(int id);
}