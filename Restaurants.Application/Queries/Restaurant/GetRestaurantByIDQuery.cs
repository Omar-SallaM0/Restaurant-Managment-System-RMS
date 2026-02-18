using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.DTO.Restaurants;

namespace Restaurants.Application.Queries.Restaurant
{
    public class GetRestaurantByIDQuery :IRequest<RestaurantDto?>
    {
        public GetRestaurantByIDQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
