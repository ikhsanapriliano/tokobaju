using Microsoft.EntityFrameworkCore;
using Tokobaju.Entities;

namespace Tokobaju.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<User> Users{ get; set; }
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}