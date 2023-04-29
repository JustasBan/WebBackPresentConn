using System.ComponentModel.DataAnnotations.Schema;
using WebBackPresentConn.Models.Enums;

namespace WebBackPresentConn.Models.Entities
{
    public class PizzaOrder
    {
        public int Id { get; set; }
        public PizzaSize Size { get; set; }
        public ICollection<Topping>? Toppings { get; set; }
        public decimal TotalCost { get; set; }

        [NotMapped]
        public List<int> ToppingIds { get; set; }
    }
}
