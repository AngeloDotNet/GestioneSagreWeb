namespace GestioneSagre.Utility.DataAccessLayer.Configurations;

public class ClienteTipoConfiguration : IEntityTypeConfiguration<ClienteTipo>
{
    public void Configure(EntityTypeBuilder<ClienteTipo> builder)
    {
        builder.ToTable("TipoCliente");
        builder.HasKey(e => e.Id);

        //builder.Property(e => e.Id).HasDefaultValueSql(Guid.NewGuid().ToString());
        builder.Property(e => e.Valore).IsRequired().HasMaxLength(255);
    }
}