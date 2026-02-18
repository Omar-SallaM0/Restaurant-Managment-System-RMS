using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.DTO.Satff;
using Restaurants.Infrastructure.Repository;

namespace Restaurants.Application.Queries.Satff;

public class GetRestaurantStaffQueryHandler(IStaffRepository _staffRepository) : IRequestHandler<GetRestaurantStaffQuery, List<StaffDTO>>
{
    public async Task<List<StaffDTO>> Handle(GetRestaurantStaffQuery request, CancellationToken cancellationToken)
    {
        var staffList = await _staffRepository
        .GetStaffByRestaurantIdAsync(request.restaurantid);

        return staffList.Select(x => new StaffDTO
        {
            UserId = x.UserId,
            UserName = x.User?.UserName!,
            Email = x.User?.Email!,
            Role = x.Role.ToString()
        }).ToList();
    }
}
