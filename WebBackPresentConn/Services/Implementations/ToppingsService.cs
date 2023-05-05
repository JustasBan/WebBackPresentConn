using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Services.Exceptions;
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
            if (await _context.Toppings.AnyAsync(t => t.Name == topping.Name))
            {
                throw new InvalidToppingException(topping);
            }

            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();
            return topping;
        }

        public async Task<IEnumerable<Topping>> AddMultipleToppingsAsync(IEnumerable<Topping> toppings)
        {
            foreach (var item in toppings)
            {
                if (await _context.Toppings.AnyAsync(t => t.Name == item.Name) || toppings.Count(t => t.Name == item.Name)>1)
                {
                    throw new InvalidToppingException(item);
                }
            }

            _context.AddRange(toppings);
            await _context.SaveChangesAsync();

            return toppings;
        }
    }
}
