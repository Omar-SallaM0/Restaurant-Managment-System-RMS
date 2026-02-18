using Resturants.Domain.Entities;
using Resturants.Domain.Enums;
namespace Resturants.Domain.Repositories;
public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllResturantsAsync();
    Task<Restaurant> GetRestaurantByIdAsync(int restaurantId);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant entity);
    Task SaveChanges();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}
