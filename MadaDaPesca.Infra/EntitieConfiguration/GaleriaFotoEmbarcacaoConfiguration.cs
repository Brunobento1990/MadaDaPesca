using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class GaleriaFotoEmbarcacaoConfiguration : IEntityTypeConfiguration<GaleriaFotoEmbarcacao>
{
    public void Configure(EntityTypeBuilder<GaleriaFotoEmbarcacao> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(x => x.Embarcacao)
            .WithMany(x => x.Galeria)
            .HasForeignKey(x => x.EmbarcacaoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
