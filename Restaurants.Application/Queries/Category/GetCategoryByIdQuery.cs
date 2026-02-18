using MediatR;
using Restaurants.Application.DTO.Category;

namespace Restaurants.Application.Queries.Category;
public class GetCategoryByIdQuery(int id, int restaurantId) : IRequest<GetCategoryByIdDTO>
{
    public int Id { get; init; } = id;
    public int RestaurantId { get; set; } = restaurantId;
}