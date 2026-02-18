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

public class CreateCategoryCommandHandler(IRestaurantRepository _restaurantsRepository, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger,
    IRestaurantAuthorizationService _authorizationService) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var restaurant = await _restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!_authorizationService.Authorize(restaurant, ResourceOperation.Create))
                throw new UnAuthorizedException("You are not authorized to add category to this restaurant.");

            logger.LogInformation("Creating new category for restaurant with id {RestaurantId}", request.RestaurantId);

            var category = mapper.Map<Category>(request);

            restaurant.Categories.Add(category);
            await _restaurantsRepository.SaveChanges();

            logger.LogInformation("New category created successfully with ID: {CategoryId}", category.Id);

            return category.Id;
        }
        catch (UnAuthorizedException ex)
        {
            throw;
        }
        catch (NotFoundException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding new category to restaurant with id {RestaurantId}", request.RestaurantId);
            throw;
        }
    }
}
