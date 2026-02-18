using MediatR;
using Restaurants.Application.DTO.Satff;

namespace Restaurants.Application.Queries.Satff
{
    public record GetRestaurantStaffQuery(int restaurantid) :IRequest<List<StaffDTO>>;
}
