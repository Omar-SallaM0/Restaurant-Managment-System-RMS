using MediatR;
using Restaurants.Application.DTO.Dish;

namespace Restaurants.Application.Queries.Dish;
public class GetDishesQuery(int restaurantid) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantid;
}
