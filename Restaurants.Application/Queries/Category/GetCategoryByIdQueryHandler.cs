using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Category;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Service;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Queries.Category;

public class GetCategoryByIdQueryHandler(IMapper mapper, ICategoryRepository categoriesRepository, ILogger<GetCategoryByIdQueryHandler> logger, IRestaurantRepository restaurantRepository, IRestaurantAuthorizationService authorizationService) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdDTO>
{
    public async Task<GetCategoryByIdDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!authorizationService.Authorize(restaurant, ResourceOperation.Read))
                throw new UnAuthorizedException("You are not authorized to access this restaurant's data.");

            logger.LogInformation("Getting category by id {CategoryId} for restaurant {RestaurantId}", request.Id, request.RestaurantId);

            var category = await categoriesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (category.RestaurantId != request.RestaurantId)
                throw new UnAuthorizedException("Category does not belong to this restaurant.");

            var dto = mapper.Map<GetCategoryByIdDTO>(category);
            return dto;
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
            logger.LogError(ex, "Error occurred while getting category by id {CategoryId}", request.Id);
            throw;
        }
    }
}
