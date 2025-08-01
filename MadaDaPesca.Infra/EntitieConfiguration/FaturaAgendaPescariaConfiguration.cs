using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class FaturaAgendaPescariaConfiguration : BaseEntityConfiguration<FaturaAgendaPescaria>
{
    public override void Configure(EntityTypeBuilder<FaturaAgendaPescaria> builder)
    {
        builder.Property(x => x.Valor)
            .IsRequired()
            .HasPrecision(12, 2);

        builder.Property(x => x.Descricao)
            .HasMaxLength(255);

        builder.Ignore(x => x.ValorRecebido);
        builder.Ignore(x => x.ValorAReceber);
        builder.Ignore(x => x.Vencida);
        builder.Ignore(x => x.Quitada);

        base.Configure(builder);
    }
}
