using System.Reflection;
using GestioneSagre.Categorie.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Categorie.DataAccessLayer;

public class CategorieDbContext : DbContext
{
    public CategorieDbContext(DbContextOptions<CategorieDbContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorie { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Mapping per gli owned types
        //modelBuilder.Owned<Price>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}