using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductGrpcService.Domain.Models;

namespace ProductGrpcService.Database;

public class AppDbContext : DbContext
{
    public DbSet<ProductModel> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}