using Microsoft.EntityFrameworkCore;
using WebBackPresentConn.Models.Entities;
using WebBackPresentConn.Models.Enums;
using WebBackPresentConn.Services.Interfaces;

namespace WebBackPresentConn.Services.Implementations
{
    public class PizzaOrderService : IPizzaOrderService
    {
        private readonly PizzaOrderContext _context;

        public PizzaOrderService(PizzaOrderContext context)
        {
            _context = context;
        }

        public async Task<PizzaOrder> AddPizzaOrderAsync(PizzaOrder pizzaOrder)
        {
            decimal baseCost = pizzaOrder.Size switch
            {
                PizzaSize.Small => 8,
                PizzaSize.Medium => 10,
                PizzaSize.Large => 12,
                _ => throw new ArgumentOutOfRangeException()
            };

            decimal totalCost = baseCost + pizzaOrder.Toppings.Count;

            if (pizzaOrder.Toppings.Count > 3)
            {
                totalCost *= 0.9m; // Apply 10% discount
            }

            pizzaOrder.TotalCost = totalCost;
            _context.PizzaOrders.Add(pizzaOrder);
            await _context.SaveChangesAsync();

            return pizzaOrder;
        }

        public async Task<List<PizzaOrder>> GetAllOrdersAsync()
        {
            return await _context.PizzaOrders.Include(o => o.Toppings).ToListAsync();
        }

        public async Task<PizzaOrder> GetPizzaOrderByIdAsync(int id)
        {
            return await _context.PizzaOrders.Include(o => o.Toppings).FirstOrDefaultAsync(o => o.Id == id);
        }
    }

}
