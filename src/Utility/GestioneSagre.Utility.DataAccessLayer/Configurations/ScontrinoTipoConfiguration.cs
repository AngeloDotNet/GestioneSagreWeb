namespace GestioneSagre.Utility.DataAccessLayer.Configurations;

public class ScontrinoTipoConfiguration : IEntityTypeConfiguration<ScontrinoTipo>
{
    public void Configure(EntityTypeBuilder<ScontrinoTipo> builder)
    {
        builder.ToTable("TipoScontrino");
        builder.HasKey(e => e.Id);

        //builder.Property(e => e.Id).HasDefaultValueSql(Guid.NewGuid().ToString());
        builder.Property(e => e.Valore).IsRequired().HasMaxLength(255);
    }
}