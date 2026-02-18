using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Infrastructure.Repository;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;
using Resturants.Domainntities;

namespace Restaurants.Application.Commands;

public class DeleteStaffCommandHandler(ILogger<DeleteStaffCommandHandler> logger, IStaffRepository staffRepo, IRestaurantRepository restaurantRepo, IUserContext userContext) : IRequestHandler<DeleteStaffCommand>
{
    public async Task Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = userContext.GetCurrentUser()
                ?? throw new UnAuthorizedException();

            var currentUserId = currentUser.Id;

            logger.LogInformation(
                "User with id: {userId} is deleting staff with id: {staffId}",
                currentUserId,
                request.StaffId);

            var staff = await staffRepo.GetById(request.StaffId)
                ?? throw new NotFoundException(nameof(Staff),"Staff not found");

            var restaurant = await restaurantRepo.GetRestaurantByIdAsync(staff.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant),"Restaurant not found");

            if (restaurant.OwnerId != currentUserId)
                throw new ForbidException();

            staffRepo.Delete(staff);
            await staffRepo.SaveChanges();

            logger.LogInformation("Staff with id: {staffId} deleted successfully.",request.StaffId);
        }
        catch (UnAuthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete staff due to exception");
            throw;
        }
    }
}

