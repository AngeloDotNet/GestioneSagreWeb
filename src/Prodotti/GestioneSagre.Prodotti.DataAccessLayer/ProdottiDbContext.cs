﻿namespace GestioneSagre.Prodotti.DataAccessLayer;

public class ProdottiDbContext : DbContext
{
    public ProdottiDbContext(DbContextOptions<ProdottiDbContext> options) : base(options)
    {
    }

    public DbSet<Prodotto> Prodotti { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Mapping per gli owned types
        modelBuilder.Owned<Price>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}