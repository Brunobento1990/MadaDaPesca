using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class AcessoGuiaDePescaConfiguration : IEntityTypeConfiguration<AcessoGuiaDePesca>
{
    public void Configure(EntityTypeBuilder<AcessoGuiaDePesca> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Senha)
            .IsRequired()
            .HasMaxLength(1000);
    }
}
