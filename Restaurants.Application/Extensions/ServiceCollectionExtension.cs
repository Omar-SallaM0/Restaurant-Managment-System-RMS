using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services.RestauarantServiceAutoMapper;
using Restaurants.Application.Services.RestaurantDtoService;
using Restaurants.Application.Services.RestaurantService;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;
public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantDtoService, RestaurantDtoService>();
        services.AddScoped<IRestauarantServiceAutoMapper, RestauarantServiceAutoMapper>();
        services.AddScoped<IUserContext, UserContext>();

        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly).AddFluentValidationAutoValidation();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        services.AddHttpContextAccessor();
     
    }
}
