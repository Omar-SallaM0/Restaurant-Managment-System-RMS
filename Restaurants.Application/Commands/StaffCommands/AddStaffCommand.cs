using MediatR;
using Restaurants.Application.Users;
using Restaurants.Infrastructure.Repository;
using Resturants.Domain.Enums;

namespace Restaurants.Application.Commands.StaffCommands;
public class AddStaffCommand : IRequest<int>
{
    public int RestaurantId { get; set; }
    public string UserId { get; set; } = default!;
    public StaffRole Role { get; set; }
}
