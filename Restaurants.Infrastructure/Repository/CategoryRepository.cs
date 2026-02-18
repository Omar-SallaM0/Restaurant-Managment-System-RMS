using Microsoft.EntityFrameworkCore;
using Restaurants.Infrastructure.persistence;
using Resturants.Domain.Entities;
using Resturants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repository;

public class CategoryRepository(RestaurantDBContext _db) : ICategoryRepository
{
    public async Task<int> CommitAsync()
    {
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);
        return await CommitAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _db.Categories.AnyAsync(c => c.Id == id);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Category?> GetByIdWithDishesAsync(int id)
    {
        return _db.Categories
            .Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Dishes = c.Dishes.Select(d => new Dish { id = d.id }).ToList()
            })
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetByRestaurantId(int restaurantId)
    {
        return await _db.Categories
            .Where(c => c.RestaurantId == restaurantId)
            .ToListAsync();
    }
}
