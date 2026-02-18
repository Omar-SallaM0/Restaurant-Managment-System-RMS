using MediatR;

namespace Restaurants.Application.Commands.RestaurantCommands.DeleteRestaurant;
public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
