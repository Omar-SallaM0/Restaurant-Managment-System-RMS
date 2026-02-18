using FluentValidation;
using FluentValidation.Validators;
using Resturants.Domain.Entities;

namespace Restaurants.Application.Commands.RestaurantCommands.Validator;
public class CreateRestaurantValidator :AbstractValidator<Restaurant>
{
    public CreateRestaurantValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name Mustn't Empty");
        RuleFor(dto => dto.ConatactEmail).Matches(@"Regular Experression");

    }
}
