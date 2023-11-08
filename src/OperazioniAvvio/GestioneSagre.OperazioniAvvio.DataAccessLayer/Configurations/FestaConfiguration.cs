using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Configurations;

public class FestaConfiguration : IEntityTypeConfiguration<Festa>
{
    public void Configure(EntityTypeBuilder<Festa> builder)
    {
        builder.ToTable("Feste");
        builder.HasKey(e => e.Id);
        builder.Property(f => f.StatusFesta).HasConversion<string>();

        builder.Property(f => f.DataInizio).IsRequired();
        builder.Property(f => f.DataFine).IsRequired();

        builder.HasMany(f => f.Intestazioni)
            .WithOne(i => i.Festa)
            .HasForeignKey(i => i.IdFesta)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Impostazioni)
            .WithOne(i => i.Festa)
            .HasForeignKey(i => i.IdFesta)
            .OnDelete(DeleteBehavior.Cascade);
    }
}