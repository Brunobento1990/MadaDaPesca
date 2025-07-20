using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class GuiaDePescaConfiguration : BaseEntityConfiguration<GuiaDePesca>
{
    public override void Configure(EntityTypeBuilder<GuiaDePesca> builder)
    {
        builder.HasOne(x => x.Pessoa)
            .WithOne()
            .HasForeignKey<GuiaDePesca>(x => x.PessoaId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        builder.HasOne(x => x.AcessoGuiaDePesca)
            .WithOne()
            .HasForeignKey<GuiaDePesca>(x => x.AcessoGuiaDePescaId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        builder.HasMany(x => x.Pescarias)
            .WithOne(x => x.GuiaDePesca)
            .HasForeignKey(x => x.GuiaDePescaId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        base.Configure(builder);
    }
}
