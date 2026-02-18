using FluentValidation;
using Restaurants.Application.Commands.DishesCommands.CreateDish;

namespace Restaurants.Application.Commands.DishesCommands.DishValidator;
public class CreateDishValidator :AbstractValidator<CreateDishCommand>
{
    public CreateDishValidator()
    {
        RuleFor(dish=>dish.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Not Empty Not String");
    }
}
