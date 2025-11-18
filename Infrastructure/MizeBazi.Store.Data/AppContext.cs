using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Data.Entities;
using System.Text.Json;

namespace MizeBazi.Store.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            AppSetings.SetConnection();
            optionsBuilder.UseSqlServer(AppSetings.WriteConnection, sql =>
            {
                sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }

    internal DbSet<Category> Categorys { get; set; }
    internal DbSet<Brand> Brands { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<Cart> Carts { get; set; }
    internal DbSet<CartItem> CartItems { get; set; }
    internal DbSet<Order> Orders { get; set; }
    internal DbSet<OrderItem> OrderItems { get; set; }
    internal DbSet<Payment> Payments { get; set; }
    internal DbSet<ProductCategory> ProductCategorys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
