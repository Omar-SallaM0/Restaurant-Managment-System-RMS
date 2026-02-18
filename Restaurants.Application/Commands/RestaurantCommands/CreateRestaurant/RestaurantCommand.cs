using MediatR;
using Restaurants.Application.DTO.Dish;
using Resturants.Domain.Entities;

namespace Restaurants.Application.Commands.RestaurantCommands.CreateRestaurant;
public class RestaurantCommand :IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? ConatactPhone { get; set; }
    public string? ConatactEmail { get; set; }
    public Address? address { get; set; } = new();
}
