namespace PizzaOrderApi.Services.Exceptions
{
    public class NoToppingsException : Exception
    {
        public NoToppingsException() : base("A pizza must have at least one topping.")
        {
        }
    }

}
