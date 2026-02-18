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

public class GetAllCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoriesRepository, ILogger<GetCategoryByIdQueryHandler> logger, IRestaurantRepository restaurantRepository, IRestaurantAuthorizationService authorizationService) : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesDTO>>
{
    public async Task<List<GetAllCategoriesDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!authorizationService.Authorize(restaurant, ResourceOperation.Read))
                throw new UnAuthorizedException("You are not authorized to access this restaurant's categories.");

            logger.LogInformation("Getting categories for RestaurantId: {RestaurantId}", request.RestaurantId);

            var categories = await categoriesRepository.GetByRestaurantId(request.RestaurantId);

            var dto = mapper.Map<List<GetAllCategoriesDTO>>(categories);
            return dto;
        }
        catch (UnAuthorizedException )
        {
            throw;
        }
        catch (NotFoundException )
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while getting categories for RestaurantId: {RestaurantId}", request.RestaurantId);
            throw;
        }
    }
}

