using AutoMapper;
using Restaurants.Application.Commands.DishesCommands.CreateDish;
using Restaurants.Application.DTO.Dish;
using Resturants.Domain.Entities;

namespace Restaurants.Application.AutoMapper;
public class DishProfile :Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<Dish, CreateDishCommand>();
        CreateMap<CreateDishCommand, Dish>();
    }
}
