namespace PizzaOrderApi.Services.Exceptions
{
    public class NoSizeException : Exception
    {
        public NoSizeException() : base("A pizza must have a size.")
        {
        }
    }

}
