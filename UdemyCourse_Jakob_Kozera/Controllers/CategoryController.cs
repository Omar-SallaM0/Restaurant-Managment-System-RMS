using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.CategoryCommand;
using Restaurants.Application.DTO.Category;
using Restaurants.Application.Queries.Category;
using Resturants.Domain.Enums;

namespace Restaurants.Api.Controllers
{
    [Authorize]
    [Route("api/restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllCategoriesDTO>>> GetAll(int restaurantId)
        {
            var dto = await _mediator.Send(new GetAllCategoriesQuery(restaurantId));
            return dto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryByIdDTO>> GetById(int restaurantId, int id)
        {
            var dto = await _mediator.Send(new GetCategoryByIdQuery(id, restaurantId));
            return dto;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Update(int restaurantId, int id, UpdateCategoryCommand command)
        {
            command.Id = id;
            command.RestaurantId = restaurantId;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(int restaurantId, int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id, restaurantId));
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Create(int restaurantId, CreateCategoryCommand command)
        {
            command.RestaurantId = restaurantId;
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id, restaurantId }, id);
        }
    }
}
