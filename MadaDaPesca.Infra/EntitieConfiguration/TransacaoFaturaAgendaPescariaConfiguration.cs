using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class TransacaoFaturaAgendaPescariaConfiguration : BaseEntityConfiguration<TransacaoFaturaAgendaPescaria>
{
    public override void Configure(EntityTypeBuilder<TransacaoFaturaAgendaPescaria> builder)
    {
        builder.Property(x => x.Valor)
            .IsRequired()
            .HasPrecision(12, 2);

        builder.Property(x => x.Descricao)
            .HasMaxLength(255);

        builder.HasIndex(x => x.MeioDePagamento);
        builder.HasIndex(x => x.TipoTransacao);

        base.Configure(builder);
    }
}
