using Microsoft.AspNetCore.Identity;
using Restaurants.Infrastructure.persistence;
using Resturants.Domain.Entities;
using Resturants.Domain.Enums;
namespace Restaurants.Infrastructure.Seeders;
internal class RestaurantSeeder(RestaurantDBContext restaurantDBContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await restaurantDBContext.Database.CanConnectAsync())
        {
            if (!restaurantDBContext.Restaurants.Any())
            {
                var Restaurants = GetRestaurants();
                restaurantDBContext.Restaurants.AddRange(Restaurants);
                await restaurantDBContext.SaveChangesAsync();
            }
            if (!restaurantDBContext.Roles.Any())
            {
                var roles = GetRoles();
                restaurantDBContext.Roles.AddRange(roles);
                await restaurantDBContext.SaveChangesAsync();
            }
        }
    }
    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                 new(UserRoles.User.ToString()){
                     NormalizedName=UserRoles.User.ToString().ToUpper()
                 },
                 new(UserRoles.Owner.ToString()){
                     NormalizedName=UserRoles.Owner.ToString().ToUpper()
                 },
                 new(UserRoles.Admin.ToString()){
                     NormalizedName=UserRoles.Admin.ToString().ToUpper()
                 }
            ];
        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
            new(){
                Name="KFC",
                Description="The F1 Restaurant Popular in world BUT It BOYCOT",
                Categories=[new Category(){
                                Name ="Junk Food",
                                Description="It's Unhealthy Food",
                                RestaurantId=1,
                               Dishes= {new Dish(){
                                    Name="burger",
                                    Description="It's From Chicken",
                                    Price=150}
                    }
                }
                           ],
                ConatactEmail="kfc@gmail.com",
                HasDelivery=true,
                address= new Address(){
                    City="Britsh",Street="Sallam's",PostalCode="100"
                },
                Dishes=[
                    new(){
                        Name="soccei",
                        Description="it's From Fishes",
                        Price=300
                    },
                    new(){
                        Name="spagetti",
                        Description="Pasta Title from Italia",
                        Price=200
                    }
                    ]
            },
            new(){
                Name="MC",
                Description="BOYCOTT To Support Our Brother Palestine",
                Categories=[
                    new Category(){
                                Name ="Junk Food",
                                Description="It's Unhealthy Food",
                                RestaurantId=2,
                               Dishes= {new Dish(){
                                    Name="Zommz",
                                    Description="It's From Dog",
                                    Price=50}
                    } }
                    ],
                ConatactEmail="mc@gmail.com",
                HasDelivery=true,
                address= new Address(){
                    City="London",
                    Street="Sallam's"
                    ,PostalCode="220"
                },
                Dishes=[
                    new(){
                        Name="chicken",
                        Description="dinner after ray",
                        Price=450
                    },
                    new(){
                        Name="Meat",
                        Description="klsdkfj",
                        Price=400
                    }
                    ]
            }
            ];
        return restaurants;
    }
}
