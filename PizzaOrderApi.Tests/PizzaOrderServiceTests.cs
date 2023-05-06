using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Services.Implementations;
using PizzaOrderApi.Services.Interfaces;
using WebBackPresentConn.Models.Entities;
using WebBackPresentConn.Models.Enums;
using WebBackPresentConn.Services.Implementations;
using WebBackPresentConn.Services.Interfaces;

namespace PizzaOrderApi.Tests
{
    public class PizzaOrderServiceTests
    {
        private readonly PizzaOrderContext _context;
        private readonly IToppingsService _toppingsService;
        private readonly IPizzaOrderService _pizzaOrderService;

        public PizzaOrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaOrderContext>()
                .UseInMemoryDatabase(databaseName: "PizzaOrderServiceTests")
                .Options;
            _context = new PizzaOrderContext(options);
            _toppingsService = new ToppingsService(_context);
            _pizzaOrderService = new PizzaOrderService(_context, _toppingsService);


            _toppingsService.AddMultipleToppingsAsync (
                new List<Topping>
                {
                    new Topping { Name = "Pepperoni" },
                    new Topping { Name = "Mushrooms" },
                    new Topping { Name = "Onions" }
                });
        }

        [Fact]
        public async Task EstimateCostAsync_DiscountCost()
        {
            // Arrange
            var service = _pizzaOrderService;
            var size = PizzaSize.Medium;
            var toppings = new List<int>{1,2,1,1};

            // Act
            var estimatedCost = await service.EstimateCostAsync(size, toppings);

            // Assert
            Assert.Equal(12.6m, estimatedCost);
        }

        [Fact]
        public async Task EstimateCostAsync_FullCost()
        {
            // Arrange
            var service = _pizzaOrderService;
            var size = PizzaSize.Medium;
            var toppings = new List<int> { 1, 2, 1 };

            // Act
            var estimatedCost = await service.EstimateCostAsync(size, toppings);

            // Assert
            Assert.Equal(13m, estimatedCost);
        }

    }
}