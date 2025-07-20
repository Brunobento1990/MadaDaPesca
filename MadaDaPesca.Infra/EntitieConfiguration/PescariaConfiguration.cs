using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class PescariaConfiguration : BaseEntityConfiguration<Pescaria>
{
    public override void Configure(EntityTypeBuilder<Pescaria> builder)
    {
        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.Local)
            .IsRequired()
            .HasMaxLength(100);
        base.Configure(builder);
    }
}
