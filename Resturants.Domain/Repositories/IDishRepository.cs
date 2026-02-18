using Resturants.Domain.Entities;

namespace Resturants.Domain.Repositories;
public interface IDishRepository
{
    Task<IEnumerable<Dish>> GetAllDishes();
    Task<Dish> GetDishById(int id);
    Task<int> Create(Dish dish);
    Task Delete(int dishID);
    Task DeleteAllDishes(IEnumerable<Dish> dishes); 

}
