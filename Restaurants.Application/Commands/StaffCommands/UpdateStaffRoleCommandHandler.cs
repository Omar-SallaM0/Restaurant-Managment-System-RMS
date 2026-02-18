using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Infrastructure.Repository;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Commands;

public class UpdateStaffRoleCommandHandler(ILogger<UpdateStaffRoleCommandHandler> logger, IStaffRepository staffRepo, IRestaurantRepository restaurantRepo, IUserContext userContext) : IRequestHandler<UpdateStaffRoleCommand>
{
    public async Task Handle(UpdateStaffRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = userContext.GetCurrentUser() ?? throw new UnAuthorizedException();
            var currentUserId = currentUser.Id;

            logger.LogInformation("User with id: {userId} is updating staff with id: {staffId}",currentUserId,request.StaffId);

            var staff = await staffRepo.GetById(request.StaffId)
                ?? throw new NotFoundException(nameof(Restaurant), request.Role.ToString());

            var restaurant = await restaurantRepo.GetRestaurantByIdAsync(staff.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.Role.ToString());

            if (restaurant.OwnerId != currentUserId)
                throw new ForbidException();

            staff.UpdateRole(request.Role);

            await staffRepo.SaveChanges();

            logger.LogInformation("Staff with id: {staffId} updated successfully",request.StaffId);
        }
        catch (UnAuthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update staff due to exception");
            throw;
        }
    }
}
