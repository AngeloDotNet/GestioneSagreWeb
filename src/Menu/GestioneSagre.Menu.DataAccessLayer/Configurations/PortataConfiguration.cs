using GestioneSagre.Menu.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.Menu.DataAccessLayer.Configurations;

public class PortataConfiguration : IEntityTypeConfiguration<Portata>
{
    public void Configure(EntityTypeBuilder<Portata> builder)
    {
        builder.ToTable("Portate");
        builder.HasKey(e => e.Id);

        builder.Property(p => p.IdFesta).IsRequired();
        builder.Property(p => p.IdProdotto).IsRequired();
        builder.Property(p => p.DataMenu).IsRequired();
    }
}