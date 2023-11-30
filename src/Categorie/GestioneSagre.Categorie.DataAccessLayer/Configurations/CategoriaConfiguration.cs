using GestioneSagre.Categorie.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.Categorie.DataAccessLayer.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorie");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CategoriaVideo).IsRequired().HasMaxLength(30);
        builder.Property(c => c.CategoriaStampa).IsRequired().HasMaxLength(30);
    }
}