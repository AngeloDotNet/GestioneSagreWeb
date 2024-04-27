namespace GestioneSagre.Utility.DataAccessLayer;

public class UtilityDbContext : DbContext
{
    public UtilityDbContext(DbContextOptions<UtilityDbContext> options) : base(options)
    {
    }

    public DbSet<ClienteTipo> ClientiTipo { get; set; }
    public DbSet<PagamentoTipo> PagamentiTipo { get; set; }
    public DbSet<ScontrinoStato> ScontriniStato { get; set; }
    public DbSet<ScontrinoTipo> ScontriniTipo { get; set; }
    public DbSet<Valuta> Valute { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ClienteTipo>()
            .HasData(new ClienteTipo { Id = Guid.NewGuid(), Valore = "CLIENTE" },
            new ClienteTipo { Id = Guid.NewGuid(), Valore = "STAFF" });

        modelBuilder.Entity<PagamentoTipo>()
            .HasData(new PagamentoTipo { Id = Guid.NewGuid(), Valore = "CONTANTI" });

        modelBuilder.Entity<ScontrinoStato>()
            .HasData(new ScontrinoStato { Id = Guid.NewGuid(), Valore = "APERTO" },
            new ScontrinoStato { Id = Guid.NewGuid(), Valore = "IN ELABORAZIONE" },
            new ScontrinoStato { Id = Guid.NewGuid(), Valore = "DA INCASSARE" },
            new ScontrinoStato { Id = Guid.NewGuid(), Valore = "PAGATO" },
            new ScontrinoStato { Id = Guid.NewGuid(), Valore = "CHIUSO" },
            new ScontrinoStato { Id = Guid.NewGuid(), Valore = "ANNULLATO" });

        modelBuilder.Entity<ScontrinoTipo>()
            .HasData(new ScontrinoTipo { Id = Guid.NewGuid(), Valore = "PAGAMENTO" },
            new ScontrinoTipo { Id = Guid.NewGuid(), Valore = "OMAGGIO" });

        modelBuilder.Entity<Valuta>()
            .HasData(new Valuta { Id = Guid.NewGuid(), Valore = "EUR", Descrizione = "Euro", Simbolo = "€" },
            new Valuta { Id = Guid.NewGuid(), Valore = "USD", Descrizione = "Dollaro USA", Simbolo = "$" },
            new Valuta { Id = Guid.NewGuid(), Valore = "GBP", Descrizione = "Sterlina UK", Simbolo = "£" });
    }
}