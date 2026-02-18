using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Resturants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager,roleManager,options)
{
    public async override Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var Person = await GenerateClaimsAsync(user);

        if (user.Nationality != null)
        {
            Person.AddClaim(new Claim("Nationality", user.Nationality));
        }
        if (user.DateOfBirth != null)
        {
            Person.AddClaim(new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }
        return new ClaimsPrincipal(Person);
    }
}
