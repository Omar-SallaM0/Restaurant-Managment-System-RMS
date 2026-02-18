using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
namespace Restaurants.Infrastructure.Authorization.Requirement;
public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,IUserContext userContext)
    : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentuser =userContext.GetCurrentUser();
        logger.LogInformation("User : {UserEmail} Id : {UserId} , Date Of Birth {DoB} - Handling MinimumRequirementAge"
            , currentuser?.Email, currentuser?.Id, currentuser?.DateOfBirth);
        if (currentuser?.DateOfBirth == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        if (currentuser.DateOfBirth.Value.AddYears(requirement.MinAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
