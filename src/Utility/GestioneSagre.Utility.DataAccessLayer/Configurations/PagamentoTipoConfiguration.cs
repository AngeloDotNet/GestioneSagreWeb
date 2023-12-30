using GestioneSagre.Utility.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.Utility.DataAccessLayer.Configurations;

public class PagamentoTipoConfiguration : IEntityTypeConfiguration<PagamentoTipo>
{
    public void Configure(EntityTypeBuilder<PagamentoTipo> builder)
    {
        builder.ToTable("TipoPagamento");
        builder.HasKey(e => e.Id);

        //builder.Property(e => e.Id).HasDefaultValueSql(Guid.NewGuid().ToString());
        builder.Property(e => e.Valore).IsRequired().HasMaxLength(255);
    }
}