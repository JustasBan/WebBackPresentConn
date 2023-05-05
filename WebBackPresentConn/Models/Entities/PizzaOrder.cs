using PizzaOrderApi.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebBackPresentConn.Models.Enums;

namespace WebBackPresentConn.Models.Entities
{
    public class PizzaOrder
    {
        public int Id { get; set; }
        public PizzaSize Size { get; set; }
        public decimal TotalCost { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public List<int> ToppingIds { get; set; }

        [NotMapped]
        public IReadOnlyList<Topping>? ToppingsList => PizzaOrderToppings?.Select(pt => pt.Topping).ToList();

        [JsonIgnore]
        public ICollection<PizzaOrderTopping>? PizzaOrderToppings { get; set; }
    }
}
