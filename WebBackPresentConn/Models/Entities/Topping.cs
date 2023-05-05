
using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Models.Entities;
using System.Text.Json.Serialization;

namespace WebBackPresentConn.Models.Entities
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<PizzaOrderTopping>? PizzaOrderToppings { get; set; }
    }
}
