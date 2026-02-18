using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Service;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Commands.CategoryCommand;

public class UpdateCategoryCommandHandler(ICategoryRepository categoriesRepository, ILogger<UpdateCategoryCommandHandler> logger, IRestaurantRepository restaurantsRepository, IRestaurantAuthorizationService authorizationService) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new UnAuthorizedException("You are not authorized to update category of this restaurant.");

            var category = await categoriesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (category.RestaurantId != request.RestaurantId)
                throw new UnAuthorizedException("Category does not belong to this restaurant.");

            logger.LogInformation("Updating category with id {CategoryId}", request.Id);

            category.Name = request.Name;
            category.Description = request.Description;

            await categoriesRepository.CommitAsync();
            logger.LogInformation("Category with id {CategoryId} updated successfully", request.Id);
        }
        catch (UnAuthorizedException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while updating category with id {CategoryId}", request.Id);
            throw;
        }
    }
}

