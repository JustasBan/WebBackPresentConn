using WebBackPresentConn.Models.Entities;

namespace PizzaOrderApi.Services.Exceptions
{
    public class InvalidToppingException : Exception
    {
        public InvalidToppingException(Topping topping) : base()
        {
            Topping = topping;
        }

        public Topping Topping { get; internal set; }
    }
}
