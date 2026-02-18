using AutoMapper;
using Restaurants.Application.Commands.RestaurantCommands.CreateRestaurant;
using Restaurants.Application.Commands.StaffCommands;
using Restaurants.Application.DTO.Restaurants;
using Resturants.Domain.Entities;
using Resturants.Domainntities;

namespace Restaurants.Application.AutoMapper;
public class RestauarntProfile : Profile
{
    public RestauarntProfile()
    {
        CreateMap<Restaurant,RestaurantDto>();
        CreateMap<RestaurantCommand, Restaurant>();
        CreateMap<Restaurant, RestaurantCommand>(); 
        CreateMap<AddStaffCommand, Staff>();


    }
}
