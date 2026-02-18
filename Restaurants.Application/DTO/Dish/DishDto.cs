namespace Restaurants.Application.DTO.Dish;
using Resturants.Domain.Entities;
public class DishDto
{
    public int id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }

    public static DishDto FromEntity(Dish entity)
    {
        return new DishDto
        {
            id = entity.id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            RestaurantId = entity.RestaurantId
        };
    }
}
