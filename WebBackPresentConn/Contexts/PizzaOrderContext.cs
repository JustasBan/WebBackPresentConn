using Microsoft.EntityFrameworkCore;
using PizzaOrderApi.Models.Entities;
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
        modelBuilder.Entity<PizzaOrderTopping>()
            .HasKey(pt => pt.Id);

        modelBuilder.Entity<PizzaOrderTopping>()
            .HasOne(pt => pt.PizzaOrder)
            .WithMany(po => po.PizzaOrderToppings)
            .HasForeignKey(pt => pt.PizzaOrderId);

        modelBuilder.Entity<PizzaOrderTopping>()
            .HasOne(pt => pt.Topping)
            .WithMany(t => t.PizzaOrderToppings)
            .HasForeignKey(pt => pt.ToppingId);

        modelBuilder.Entity<Topping>()
            .HasIndex(t => t.Name)
            .IsUnique();
    }
}