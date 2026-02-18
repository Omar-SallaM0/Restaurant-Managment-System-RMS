using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Restaurants.Application.Services.RestaurantDtoService;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsDtosController(IRestaurantDtoService repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var res = await repo.GetAllRestaurant();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var res = await repo.GetAllRestaurantByID(id);
            return Ok(res);
        }
    }
}
