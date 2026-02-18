using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.DishesCommands.CreateDish;
using Restaurants.Application.Commands.DishesCommands.DeleteDish;
using Restaurants.Application.DTO.Dish;
using Restaurants.Application.Queries.Dish;

namespace Restaurants.Api.Controllers;
[Route("api/restaurant/{restaurantid}/dishes")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute]int restaurantid,CreateDishCommand dishCommand)
    {
        dishCommand.RestaurantId = restaurantid;
        await mediator.Send(dishCommand);
        return Created();
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishes([FromRoute] int restaurantid)
    {
        var dishes = await mediator.Send(new GetDishesQuery(restaurantid));
        return Ok(dishes);
    }
    [HttpGet("{dishId}")]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetDishesById([FromRoute] int restaurantid,[FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdQuery(restaurantid,dishId));
        return Ok(dish);
    }
    [HttpDelete("{dishId}")]
    public async Task<ActionResult> DeleteDish([FromRoute] int dishId, [FromRoute] int restaurantid)
    {
        await mediator.Send(new DeleteDishCommand(dishId,restaurantid));
        return NoContent();
    }
}
