﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaOrderApi.Services.Exceptions;
using WebBackPresentConn.Models.Entities;
using WebBackPresentConn.Models.Enums;
using WebBackPresentConn.Services.Interfaces;

namespace WebBackPresentConn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaOrdersController : ControllerBase
    {
        private readonly IPizzaOrderService _pizzaOrderService;

        public PizzaOrdersController(IPizzaOrderService pizzaOrderService)
        {
            _pizzaOrderService = pizzaOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPizzaOrder(PizzaOrder pizzaOrder)
        {
            if (pizzaOrder == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPizzaOrder = await _pizzaOrderService.AddPizzaOrderAsync(pizzaOrder);
                return CreatedAtAction(nameof(GetPizzaOrderById), new { id = createdPizzaOrder.Id }, createdPizzaOrder);
            }
            catch (NoToppingsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var pizzaOrders = await _pizzaOrderService.GetAllOrdersAsync();
            return Ok(pizzaOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaOrderById(int id)
        {
            var pizzaOrder = await _pizzaOrderService.GetPizzaOrderByIdAsync(id);
            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return Ok(pizzaOrder);
        }

        [HttpPost("estimate")]
        public async Task<IActionResult> EstimateCost(PizzaSize size, List<Topping> toppings)
        {
            var estimatedCost = await _pizzaOrderService.EstimateCostAsync(size, toppings);
            return Ok(estimatedCost);
        }

        [HttpPost("sizes")]
        [HttpGet("sizes")]
public IActionResult GetPizzaSizes()
{
    return Ok(_pizzaOrderService.GetPizzaSizeNames());
}


    }
}
