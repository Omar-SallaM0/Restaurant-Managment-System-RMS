using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands;
using Restaurants.Application.Commands.StaffCommands;
using Restaurants.Application.DTO.Satff;
using Restaurants.Application.Queries.Satff;

namespace Restaurants.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController(IMediator _mediator) :ControllerBase
{
    [HttpGet("{restaurantid}")]
    public async Task<ActionResult<List<StaffDTO>>> GetAll([FromRoute] int restaurantId)
    {
        var result = await _mediator.Send(new GetRestaurantStaffQuery(restaurantId));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddStaffCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffRoleCommand command)
    {
        if (id != command.StaffId)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteStaffCommand { StaffId = id });
        return NoContent();
    }
    [HttpGet("{staffid}")]
    public async Task<IActionResult> GetById(int id)
    {
        var staff = await _mediator.Send(new GetRestaurantStaffQuery(id));
        return staff is null ? NotFound() : Ok(staff);
    }
}
