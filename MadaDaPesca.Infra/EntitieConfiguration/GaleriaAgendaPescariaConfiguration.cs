using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class GaleriaAgendaPescariaConfiguration : IEntityTypeConfiguration<GaleriaAgendaPescaria>
{
    public void Configure(EntityTypeBuilder<GaleriaAgendaPescaria> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(x => x.AgendaPescaria)
            .WithMany(x => x.Galeria)
            .HasForeignKey(x => x.AgendaPescariaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
