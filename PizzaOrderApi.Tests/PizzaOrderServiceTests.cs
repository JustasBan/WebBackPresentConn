using Microsoft.EntityFrameworkCore;
using Moq;
using PizzaOrderApi.Services.Exceptions;
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
        private readonly Mock<IToppingsService> _mockToppingsService;
        private readonly IPizzaOrderService _pizzaOrderService;

        public PizzaOrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaOrderContext>()
                .UseInMemoryDatabase(databaseName: "PizzaOrderServiceTests")
                .Options;
            _context = new PizzaOrderContext(options);
            _mockToppingsService = new Mock<IToppingsService>();
            _pizzaOrderService = new PizzaOrderService(_context, _mockToppingsService.Object);
        }

        [Fact]
        public async Task EstimateCostAsync_DiscountCost()
        {
            // Arrange
            var service = _pizzaOrderService;
            var size = PizzaSize.Medium;
            var toppings = new List<Topping>
            {
                new Topping { Id = 1 },
                new Topping { Id = 2 },
                new Topping { Id = 3 },
                new Topping { Id = 4 }
            };

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
            var toppings = new List<Topping>
            {
                new Topping { Id = 1 },
                new Topping { Id = 2 },
                new Topping { Id = 3 }
            };

            // Act
            var estimatedCost = await service.EstimateCostAsync(size, toppings);

            // Assert
            Assert.Equal(13m, estimatedCost);
        }

    }
}