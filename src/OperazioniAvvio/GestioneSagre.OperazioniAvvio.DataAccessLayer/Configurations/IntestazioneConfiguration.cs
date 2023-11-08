using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Configurations;

public class IntestazioneConfiguration : IEntityTypeConfiguration<Intestazione>
{
    public void Configure(EntityTypeBuilder<Intestazione> builder)
    {
        builder.ToTable("Intestazioni");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Titolo).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Edizione).IsRequired().HasMaxLength(100);

        builder.Property(i => i.Luogo).IsRequired().HasMaxLength(100);
    }
}