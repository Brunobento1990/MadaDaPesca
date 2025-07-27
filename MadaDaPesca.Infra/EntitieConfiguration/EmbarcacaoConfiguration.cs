using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class EmbarcacaoConfiguration : BaseEntityConfiguration<Embarcacao>
{
    public override void Configure(EntityTypeBuilder<Embarcacao> builder)
    {
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(x => x.Motor)
            .HasMaxLength(255);
        builder.Property(x => x.MotorEletrico)
            .HasMaxLength(255);
        builder.Property(x => x.Largura)
            .HasMaxLength(255);
        builder.Property(x => x.Comprimento)
            .HasMaxLength(255);

        builder.HasIndex(x => x.Nome);

        base.Configure(builder);
    }
}
