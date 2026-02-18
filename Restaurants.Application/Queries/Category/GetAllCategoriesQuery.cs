using MediatR;
using Restaurants.Application.DTO.Category;

namespace Restaurants.Application.Queries.Category;
public class GetAllCategoriesQuery(int restaurantId) : IRequest<List<GetAllCategoriesDTO>>
{
    public int RestaurantId { get; init; } = restaurantId;
}