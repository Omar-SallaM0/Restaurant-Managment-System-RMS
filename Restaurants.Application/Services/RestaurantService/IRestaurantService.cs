using Resturants.Domain.Entities;
namespace Restaurants.Application.Services.RestaurantService;
public interface IRestaurantService
{
    Task<IEnumerable<Restaurant>> GetAResturants();
    Task<Restaurant> GetRestaurantById(int id);
}