using MediatR;
using Restaurants.Application.DTO.Dish;

namespace Restaurants.Application.Queries.Dish;
public class GetDishByIdQuery(int resid,int dishid) :IRequest<DishDto>
{
    public int RestaurantId { get; } = resid;
    public int DishId { get; }  = dishid;
}
