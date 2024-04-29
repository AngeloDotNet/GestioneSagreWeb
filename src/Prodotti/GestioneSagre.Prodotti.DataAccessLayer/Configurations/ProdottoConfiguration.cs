namespace GestioneSagre.Prodotti.DataAccessLayer.Configurations;

public class ProdottoConfiguration : IEntityTypeConfiguration<Prodotto>
{
    public void Configure(EntityTypeBuilder<Prodotto> builder)
    {
        builder.ToTable("Prodotti");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Descrizione).IsRequired().HasMaxLength(100);
    }
}