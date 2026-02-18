using Resturants.Domain.Entities;

namespace Resturants.Domain.Repositories;
public interface ICategoryRepository
{
    Task<int> CommitAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<int> DeleteAsync(Category category);
    Task<Category?> GetByIdWithDishesAsync(int id);
    Task<List<Category>> GetByRestaurantId(int restaurantId);
    Task<bool> Exists(int id);
}
