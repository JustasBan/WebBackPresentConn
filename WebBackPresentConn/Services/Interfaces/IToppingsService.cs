using WebBackPresentConn.Models.Entities;

namespace PizzaOrderApi.Services.Interfaces
{
    public interface IToppingsService
    {
        Task<IEnumerable<Topping>> GetAllToppingsAsync();
        Task<Topping> AddToppingAsync(Topping topping);
        Task<IEnumerable<Topping>> AddMultipleToppingsAsync(IEnumerable<Topping> toppings);
    }
}
