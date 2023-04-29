
using System.Text.Json.Serialization;

namespace WebBackPresentConn.Models.Entities
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<PizzaOrder>? PizzaOrders { get; set; }
    }
}
