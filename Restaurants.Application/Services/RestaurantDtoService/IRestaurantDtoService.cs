namespace Restaurants.Application.Services.RestaurantDtoService;
using Restaurants.Application.DTO.Restaurants;
public interface IRestaurantDtoService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurant();
    Task<RestaurantDto> GetAllRestaurantByID(int id);
}