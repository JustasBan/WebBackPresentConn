using Microsoft.EntityFrameworkCore;
using WebBackPresentConn.Models.Entities;

namespace PizzaOrderApi.Contexts
{
    public class DataInitializer
    {
        public static void SeedToppingsData(PizzaOrderContext context)
        {
            var initialToppings = new List<Topping>
            {
                new Topping { Name = "Pepperoni" },
                new Topping { Name = "Mushrooms" },
                new Topping { Name = "Onions" }
            };

            context.AddRange(initialToppings);
            context.SaveChanges();
        }
    }
}
