using Prova__Prática_STgenetics.entity;
using Prova__Prática_STgenetics.enums;

namespace Prova__Prática_STgenetics;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Order>()
            .Metadata.FindNavigation(nameof(Order.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithMany();
    
        // Injeção dos dados "base" no Banco de Dados
        modelBuilder.Entity<Product>().HasData(
            new Product("X Burger", 5.00m, ProductCategory.Sandwich),
            new Product("X Egg", 4.50m, ProductCategory.Sandwich),
            new Product("X Bacon", 7.00m, ProductCategory.Sandwich),
            new Product("Batata frita", 2.00m, ProductCategory.Fries),
            new Product("Refrigerante", 2.50m, ProductCategory.Soda)
        );

    }
}