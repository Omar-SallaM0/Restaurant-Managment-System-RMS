using MediatR;

namespace Restaurants.Application.Commands.DishesCommands.DeleteDish;
public class DeleteDishCommand(int dishid, int restaurantid) : IRequest
{
    public int RestaurantId { get; }=restaurantid;
    public int dishId { get; }= dishid;
}
