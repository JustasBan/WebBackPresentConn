using Microsoft.AspNetCore.Mvc;
using PizzaOrderApi.Services.Interfaces;
using WebBackPresentConn.Models.Entities;
using WebBackPresentConn.Services.Interfaces;

namespace PizzaOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToppingsController : ControllerBase
    {
        private readonly IToppingsService _toppingsService;

        public ToppingsController(IToppingsService toppingsService)
        {
            _toppingsService = toppingsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToppings()
        {
            var toppings = await _toppingsService.GetAllToppingsAsync();
            return Ok(toppings);
        }

        [HttpPost]
        public async Task<IActionResult> AddTopping(Topping topping)
        {
            if (topping == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTopping = await _toppingsService.AddToppingAsync(topping);
            return CreatedAtAction(nameof(GetAllToppings), new { id = createdTopping.Id }, createdTopping);
        }
    }

}
