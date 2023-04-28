using Microsoft.EntityFrameworkCore;
using WebBackPresentConn.Models.Entities;

public class PizzaOrderContext : DbContext
{
    public PizzaOrderContext(DbContextOptions<PizzaOrderContext> options) : base(options)
    {
    }

    public DbSet<PizzaOrder> PizzaOrders { get; set; }
}