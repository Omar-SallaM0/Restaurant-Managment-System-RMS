using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Service;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Commands.CategoryCommand;

public class DeleteCategoryCommandHandler(IRestaurantRepository restaurantsRepository, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger,
    IRestaurantAuthorizationService authorizationService,ICategoryRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!authorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new UnAuthorizedException("You are not authorized to add category to this restaurant.");


            var category = await categoriesRepository.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (category.RestaurantId != request.RestaurantId)
                throw new UnAuthorizedException("Category does not belong to this restaurant.");

            logger.LogInformation("Deleting Category with ID: {CategoryId}", category.Id);

            await categoriesRepository.DeleteAsync(category);
            logger.LogInformation("Category with ID: {CategoryId} deleted successfully.", category.Id);
        }

        catch(UnAuthorizedException ex)
        {
            throw;
        }
        catch (NotFoundException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deleting category with Id {CategoryId}", request.Id);
            throw;
        }
    }
}
