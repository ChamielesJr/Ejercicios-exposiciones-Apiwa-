using Microsoft.EntityFrameworkCore;
using JsonRpcDemo.Data.Entities;

namespace JsonRpcDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Producto> Productos => Set<Producto>();
}
