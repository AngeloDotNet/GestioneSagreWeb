using GestioneSagre.OperazioniAvvio.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.OperazioniAvvio.DataAccessLayer.Configurations;

public class ImpostazioneConfiguration : IEntityTypeConfiguration<Impostazione>
{
    public void Configure(EntityTypeBuilder<Impostazione> builder)
    {
        builder.ToTable("Impostazioni");
        builder.HasKey(i => i.Id);
    }
}