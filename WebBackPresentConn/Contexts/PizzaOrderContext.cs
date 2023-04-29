using Microsoft.EntityFrameworkCore;
using WebBackPresentConn.Models.Entities;

public class PizzaOrderContext : DbContext
{
    public PizzaOrderContext(DbContextOptions<PizzaOrderContext> options) : base(options)
    {
    }

    public DbSet<PizzaOrder> PizzaOrders { get; set; }
    public DbSet<Topping> Toppings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PizzaOrder>()
            .HasMany(po => po.Toppings)
            .WithMany(t => t.PizzaOrders);
    }
}