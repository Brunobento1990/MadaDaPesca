using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class GaleriaDeTrofeuConfiguration : BaseEntityConfiguration<GaleriaDeTrofeu>
{
    public override void Configure(EntityTypeBuilder<GaleriaDeTrofeu> builder)
    {
        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(x => x.Descricao)
            .HasMaxLength(255);

        base.Configure(builder);
    }
}
