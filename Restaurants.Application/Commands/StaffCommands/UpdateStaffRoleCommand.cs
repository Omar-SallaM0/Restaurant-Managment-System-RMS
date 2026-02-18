using MediatR;
using Resturants.Domain.Enums;

namespace Restaurants.Application.Commands;
public class UpdateStaffRoleCommand : IRequest
{
    public int StaffId { get; set; }
    public StaffRole Role { get; set; }
}
