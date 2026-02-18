using Restaurants.Application.DTO.Dish;
using Resturants.Domain.Entities;

namespace Restaurants.Application.DTO.Restaurants
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<DishDto> Dishes { get; set; } = new();
        public static RestaurantDto? FromEntity(Restaurant? restaurant)
        {
            if (restaurant == null)
                throw new Exception("Not found");
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
            };
        }
    }
}
