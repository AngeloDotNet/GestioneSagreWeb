using System.Reflection;
using GestioneSagre.Menu.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Menu.DataAccessLayer;

public class MenuDbContext : DbContext
{
    public MenuDbContext(DbContextOptions<MenuDbContext> options) : base(options)
    {
    }

    public DbSet<Portata> Portate { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}