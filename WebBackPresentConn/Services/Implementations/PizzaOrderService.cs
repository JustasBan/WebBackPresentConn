using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Services.Exceptions;
using PizzaOrderApi.Services.Interfaces;
using WebBackPresentConn.Models.Entities;
using WebBackPresentConn.Models.Enums;
using WebBackPresentConn.Services.Interfaces;

namespace WebBackPresentConn.Services.Implementations
{
    public class PizzaOrderService : IPizzaOrderService
    {
        private readonly PizzaOrderContext _context;
        private readonly IToppingsService _toppingsService;

        public PizzaOrderService(PizzaOrderContext context, IToppingsService toppingsService)
        {
            _context = context;
            _toppingsService = toppingsService;
        }

        public async Task<PizzaOrder> AddPizzaOrderAsync(PizzaOrder pizzaOrder)
        {
            if (pizzaOrder.ToppingIds == null || pizzaOrder.ToppingIds.Count == 0)
            {
                throw new NoToppingsException();
            }

            var allToppings = await _toppingsService.GetAllToppingsAsync();
            var allToppingIds = allToppings.Select(t => t.Id).ToHashSet();

            var validToppings = new List<Topping>();

            foreach (var toppingId in pizzaOrder.ToppingIds)
            {
                var existingTopping = allToppings.FirstOrDefault(t => t.Id == toppingId);

                if (existingTopping == null)
                {
                    throw new InvalidToppingException(new Topping { Id = toppingId });
                }

                validToppings.Add(existingTopping);
            }

            pizzaOrder.Toppings = validToppings;
            decimal totalCost = CalculatePizzaCost(pizzaOrder.Size, pizzaOrder.Toppings.Count);
            pizzaOrder.TotalCost = totalCost;
            _context.PizzaOrders.Add(pizzaOrder);
            await _context.SaveChangesAsync();

            return pizzaOrder;
        }

        public async Task<IEnumerable<PizzaOrder>> GetAllOrdersAsync()
        {
            return await _context.PizzaOrders
                .Include(po => po.Toppings)
                .ToListAsync();
        }

        public async Task<PizzaOrder> GetPizzaOrderByIdAsync(int id)
        {
            return await _context.PizzaOrders.Include(o => o.Toppings).FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<decimal> EstimateCostAsync(PizzaSize size, List<Topping> toppings)
        {
            decimal estimatedCost = CalculatePizzaCost(size, toppings.Count);

            return Task.FromResult(estimatedCost);
        }

        private decimal CalculatePizzaCost(PizzaSize size, int toppingsCount)
        {
            decimal baseCost = size switch
            {
                PizzaSize.Small => 8,
                PizzaSize.Medium => 10m,
                PizzaSize.Large => 12,
                _ => throw new ArgumentOutOfRangeException()
            };

            decimal totalCost = baseCost + toppingsCount;

            if (toppingsCount > 3)
            {
                totalCost *= 0.9m; // Apply 10% discount
            }

            return totalCost;
        }

        public IEnumerable<string> GetPizzaSizeNames()
        {
            return Enum.GetNames(typeof(PizzaSize));
        }
    }

}
