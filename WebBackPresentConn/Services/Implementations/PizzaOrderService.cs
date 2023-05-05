using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Models.Entities;
using PizzaOrderApi.Services.Exceptions;
using PizzaOrderApi.Services.Interfaces;
using System.Drawing;
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
            var allToppingIds = allToppings.Select(t => t.Id).ToList();

            var validToppings = new List<PizzaOrderTopping>();

            foreach (var toppingId in pizzaOrder.ToppingIds)
            {
                var existingTopping = allToppings.FirstOrDefault(t => t.Id == toppingId);

                if (existingTopping == null)
                {
                    throw new InvalidToppingException(new Topping { Id = toppingId });
                }

                validToppings.Add(new PizzaOrderTopping { ToppingId = existingTopping.Id });
            }

            if (pizzaOrder.Size == PizzaSize.None)
            { 
                throw new NoSizeException();
            }

            pizzaOrder.CreatedAt = DateTime.UtcNow;
            pizzaOrder.PizzaOrderToppings = validToppings;

            decimal totalCost = CalculatePizzaCost(pizzaOrder.Size, pizzaOrder.PizzaOrderToppings.Count);
            pizzaOrder.TotalCost = totalCost;
            _context.PizzaOrders.Add(pizzaOrder);
            await _context.SaveChangesAsync();

            return pizzaOrder;
        }

        public async Task<IEnumerable<PizzaOrder>> AddMultiplePizzaOrdersAsync(IEnumerable<PizzaOrder> pizzaOrders)
        {   
            var allToppings = await _toppingsService.GetAllToppingsAsync();
            List< PizzaOrder > temp = new List< PizzaOrder >();

            foreach (var item in pizzaOrders)
            {
                if (item.ToppingIds == null || item.ToppingIds.Count == 0)
                {
                    throw new NoToppingsException();
                }
                
                var allToppingIds = allToppings.Select(t => t.Id).ToList();

                var validToppings = new List<PizzaOrderTopping>();

                foreach (var toppingId in item.ToppingIds)
                {
                    var existingTopping = allToppings.FirstOrDefault(t => t.Id == toppingId);

                    if (existingTopping == null)
                    {
                        throw new InvalidToppingException(new Topping { Id = toppingId });
                    }

                    validToppings.Add(new PizzaOrderTopping { ToppingId = existingTopping.Id });
                }

                if (item.Size == PizzaSize.None)
                {
                    throw new NoSizeException();
                }
                
                item.PizzaOrderToppings = validToppings;
                decimal totalCost = CalculatePizzaCost(item.Size, item.PizzaOrderToppings.Count);
                item.TotalCost = totalCost;

                item.CreatedAt = DateTime.UtcNow;

                temp.Add(item);
            }

            _context.PizzaOrders.AddRange(temp);
            await _context.SaveChangesAsync();

            return temp;
        }

        public async Task<IEnumerable<PizzaOrder>> GetAllOrdersAsync()
        {
            return await _context.PizzaOrders
                .Include(po => po.PizzaOrderToppings)
                .ThenInclude(pt => pt.Topping)
                .ToListAsync();
        }


        public async Task<PizzaOrder> GetPizzaOrderByIdAsync(int id)
        {
            var pizzaOrder = await _context.PizzaOrders
                .Include(o => o.PizzaOrderToppings)
                .ThenInclude(pt => pt.Topping)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (pizzaOrder != null)
            {
                pizzaOrder.ToppingIds = pizzaOrder.PizzaOrderToppings.Select(pt => pt.ToppingId).ToList();
            }
            return pizzaOrder;
        }

        public async Task<decimal> EstimateCostAsync(PizzaSize size, List<int> toppings)
        {
            var allToppings = await _toppingsService.GetAllToppingsAsync();
            var allToppingIds = allToppings.Select(t => t.Id).ToHashSet();

            foreach (var toppingId in toppings)
            {
                var existingTopping = allToppings.FirstOrDefault(t => t.Id == toppingId);

                if (existingTopping == null)
                {
                    throw new InvalidToppingException(new Topping { Id = toppingId });
                }
            }

            decimal estimatedCost = CalculatePizzaCost(size, toppings.Count);

            return estimatedCost;
        }

        private decimal CalculatePizzaCost(PizzaSize size, int toppingsCount)
        {

            decimal baseCost = size switch
            {
                PizzaSize.Small => 8m,
                PizzaSize.Medium => 10m,
                PizzaSize.Large => 12m,
                PizzaSize.None => 0m,
                _ => 0
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
