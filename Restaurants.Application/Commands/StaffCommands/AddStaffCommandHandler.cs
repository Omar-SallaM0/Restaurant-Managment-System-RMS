using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Satff;
using Restaurants.Application.Users;
using Restaurants.Infrastructure.Repository;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;
using Resturants.Domainntities;

namespace Restaurants.Application.Commands.StaffCommands;

public class AddStaffCommandHandler(ILogger<AddStaffCommandHandler> logger,IStaffRepository repo,IRestaurantRepository restaurantRepo,IMapper mapper
    ,IUserContext userContext) : IRequestHandler<AddStaffCommand, int>
{
    public async Task<int> Handle(AddStaffCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = userContext.GetCurrentUser()
                ?? throw new UnAuthorizedException();

            var currentUserId = currentUser.Id;

            logger.LogInformation("User with id: {userId} is creating staff for restaurant id: {restaurantId}",currentUserId,request.RestaurantId);

            var restaurant = await restaurantRepo.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            if (restaurant.OwnerId != currentUserId)
                throw new ForbidException();

            var staff = mapper.Map<Staff>(request);

            var id = await repo.Create(staff);

            logger.LogInformation(
                "Staff with id: {staffId} created successfully for restaurant id: {restaurantId}",
                id,
                request.RestaurantId);

            return Convert.ToInt32(id);
        }
        catch (UnAuthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create staff due to exception");
            throw;
        }
    }
}
