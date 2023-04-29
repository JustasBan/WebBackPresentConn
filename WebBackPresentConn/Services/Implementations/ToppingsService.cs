using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Services.Interfaces;
using WebBackPresentConn.Models.Entities;

namespace PizzaOrderApi.Services.Implementations
{
    public class ToppingsService : IToppingsService
    {
        private readonly PizzaOrderContext _context;

        public ToppingsService(PizzaOrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topping>> GetAllToppingsAsync()
        {
            return await _context.Toppings.ToListAsync();
        }

        public async Task<Topping> AddToppingAsync(Topping topping)
        {
            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();
            return topping;
        }
    }
}
