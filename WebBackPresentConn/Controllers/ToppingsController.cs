using Microsoft.AspNetCore.Mvc;
using PizzaOrderApi.Services.Exceptions;
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

        [HttpPost("AddTopping")]
        public async Task<IActionResult> AddTopping(Topping topping)
        {
            try
            {
                if (topping == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTopping = await _toppingsService.AddToppingAsync(topping);
                return CreatedAtAction(nameof(GetAllToppings), new { id = createdTopping.Id }, createdTopping);
            }
            catch (InvalidToppingException ex)
            {

                return BadRequest($"The topping '{ex.Topping.Name}' already exists.");
            }
        }
        
        [HttpPost("AddMultipleToppings")]
        public async Task<IActionResult> AddMultipleToppings(IEnumerable<Topping> toppings)
        {
            try
            {
                var createdToppings = await _toppingsService.AddMultipleToppingsAsync(toppings);
                return Ok(createdToppings);
            }
            catch (InvalidToppingException ex)
            {
                return BadRequest($"The topping '{ex.Topping.Name}' already exists.");
            }
        }
    }

}
