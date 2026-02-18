using MediatR;

namespace Restaurants.Application.Commands;
public class DeleteStaffCommand : IRequest
{
    public int StaffId { get; set; }
}