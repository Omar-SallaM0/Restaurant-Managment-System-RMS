using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Constants;
using Restaurants.Infrastructure.Authorization.Requirement;
using Restaurants.Infrastructure.Authorization.Service;
using Restaurants.Infrastructure.persistence;
using Restaurants.Infrastructure.Repository;
using Restaurants.Infrastructure.Seeders;
using Resturants.Domain.Entities;
using Resturants.Domain.Repositories;

namespace Restaurants.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInsfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var CS = configuration.GetConnectionString("RestaurantDB");
        services.AddDbContext<RestaurantDBContext>(op => op.UseSqlServer(CS));

        services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantDBContext>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimstype.Nationality))
            .AddPolicy(PolicyNames.AtLeast20,builder=>builder.AddRequirements(new MinimumAgeRequirement(20)));

        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        services.AddScoped<IAuthorizationHandler,MinimumAgeRequirementHandler>();
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository,RestaurantRepository>();
        services.AddScoped<IDishRepository,DishRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IStaffRepository,StaffRepository>();

    }
}
