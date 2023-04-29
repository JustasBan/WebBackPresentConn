using WebBackPresentConn.Models.Entities;

namespace PizzaOrderApi.Services.Exceptions
{
    public class InvalidToppingException : Exception
    {
        public InvalidToppingException(Topping topping) : base($"Invalid topping: {topping.Id}")
        {
        }
    }
}
