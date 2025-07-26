using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class AgendaPescariConfiguration : BaseEntityConfiguration<AgendaPescaria>
{
    public override void Configure(EntityTypeBuilder<AgendaPescaria> builder)
    {
        builder.Property(x => x.Observacao)
            .HasMaxLength(255);

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Dia);
        builder.HasIndex(x => x.Ano);
        builder.HasIndex(x => x.Mes);
        builder.HasIndex(x => new { x.Dia, x.Mes, x.Ano });

        base.Configure(builder);
    }
}
