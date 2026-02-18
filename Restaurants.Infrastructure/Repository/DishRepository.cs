using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Restaurants.Infrastructure.persistence;
using Resturants.Domain.Entities;
using Resturants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repository;
public class DishRepository(RestaurantDBContext dBContext) : IDishRepository
{
    public async Task<int> Create(Dish dish)
    {
        dBContext.Dishes.Add(dish);
        await dBContext.SaveChangesAsync();
        return dish.id;
    }

    public async Task Delete(int dishID)
    {
        var dish = dBContext.Dishes.FirstOrDefault(i=>i.id == dishID);
        dBContext.Dishes.Remove(dish);
        await dBContext.SaveChangesAsync();
    }
    public async Task DeleteAllDishes(IEnumerable<Dish> dishes)
    {
        dBContext.Dishes.RemoveRange(dishes);
        await dBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Dish>> GetAllDishes()
    {
        var dishes = dBContext.Dishes.ToList();
        return dishes;
    }

    public async Task<Dish> GetDishById(int id)
    {
        var dish = await dBContext.Dishes.SingleOrDefaultAsync(i => i.id == id);
        return dish!;
    }
}
