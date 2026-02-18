using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;

namespace Restaurants.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateCommand)
    {
        await mediator.Send(updateCommand); 
        return NoContent();
    }
    [HttpPost("userrole")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand assignCommand)
    {
        await mediator.Send(assignCommand);
        return Created();
    }
}
