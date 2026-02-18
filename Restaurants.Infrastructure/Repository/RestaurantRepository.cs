using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Infrastructure.persistence;
using Resturants.Domain.Entities;
using Resturants.Domain.Enums;
using Resturants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repository;
public class RestaurantRepository(RestaurantDBContext dBContext) : IRestaurantRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        dBContext.Restaurants.Add(restaurant);
        await dBContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dBContext.Restaurants.Remove(entity);
        await dBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllResturantsAsync()
    {
        var res = await dBContext.Restaurants.Include(v=>v.Dishes).ToListAsync();
        return res;
    }
    public async Task<Restaurant> GetRestaurantByIdAsync(int restaurantId)
    {
        var res = await dBContext.Restaurants.Include(d => d.Dishes).SingleOrDefaultAsync(i => i.Id == restaurantId);
        return res!;
    }
    public Task SaveChanges()
     => dBContext.SaveChangesAsync();
    public async Task<int> GetNumberOfOwnedRestaurants(string ownerId)
    {
        return await dBContext.Restaurants.CountAsync(r => r.OwnerId == ownerId);
    }

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase,
       int pageSize,
       int pageNumber,
       string? sortBy,
       SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dBContext
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                   || r.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description }
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
    }

}

