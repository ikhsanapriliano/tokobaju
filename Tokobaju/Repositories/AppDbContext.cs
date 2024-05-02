using Microsoft.EntityFrameworkCore;
using Tokobaju.Entities;

namespace Tokobaju.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<ProductCategory> ProductCategories {get; set; }
    public DbSet<Product> Products { get; set; }

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}