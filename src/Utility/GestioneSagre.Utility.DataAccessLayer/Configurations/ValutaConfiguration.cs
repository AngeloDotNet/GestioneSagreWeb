namespace GestioneSagre.Utility.DataAccessLayer.Configurations;

public class ValutaConfiguration : IEntityTypeConfiguration<Valuta>
{
    public void Configure(EntityTypeBuilder<Valuta> builder)
    {
        builder.ToTable("Currency");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Valore).IsRequired().HasMaxLength(3);
        builder.Property(e => e.Descrizione).IsRequired();
        builder.Property(e => e.Simbolo).IsRequired().HasMaxLength(1);
    }
}