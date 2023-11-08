using System.Reflection;
using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer;

public class OperazioniAvvioDbContext : DbContext
{
    public OperazioniAvvioDbContext(DbContextOptions<OperazioniAvvioDbContext> options) : base(options)
    {
    }

    public DbSet<Festa> Feste { get; set; }
    public DbSet<Impostazione> Impostazioni { get; set; }
    public DbSet<Intestazione> Intestazioni { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}