using WebBackPresentConn.Models.Entities;

namespace WebBackPresentConn.Services.Interfaces
{
    public interface IPizzaOrderService
    {
        Task<PizzaOrder> AddPizzaOrderAsync(PizzaOrder pizzaOrder);
        Task<List<PizzaOrder>> GetAllOrdersAsync();
        Task<PizzaOrder> GetPizzaOrderByIdAsync(int id);
    }

}
