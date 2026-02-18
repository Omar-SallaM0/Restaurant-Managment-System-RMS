using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.RestaurantCommands.CreateRestaurant;
using Restaurants.Application.Commands.RestaurantCommands.DeleteRestaurant;
using Restaurants.Application.Queries.Restaurant;
using Restaurants.Application.Services.RestaurantService;
using Restaurants.Infrastructure.Authorization.Constants;
using Resturants.Domain.Enums;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy =PolicyNames.HasNationality)]
        [Authorize(Policy =PolicyNames.AtLeast20)]

        public async Task<IActionResult> GetRestaurantById([FromRoute]int id)
        {
            var res = await mediator.Send(new GetRestaurantByIDQuery(id));
            return Ok(res);
        }
        [HttpPost("create")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CreateRestaurant(RestaurantCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id },null);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }
    }
}
