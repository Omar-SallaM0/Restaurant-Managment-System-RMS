using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTO.Restaurants;
using Resturants.Domain.Entities;
using Resturants.Domain.Exceptions;
using Resturants.Domain.Repositories;

namespace Restaurants.Application.Queries.Restaurant
{
    internal class GetRestaurantByIDQueryHandler(IRestaurantRepository repo,IMapper mapper) : IRequestHandler<GetRestaurantByIDQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto?> Handle(GetRestaurantByIDQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await repo.GetRestaurantByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }
    }
}
