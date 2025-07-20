using MadaDaPesca.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MadaDaPesca.Infra.EntitieConfiguration;

internal class PessoaConfiguration : BaseEntityConfiguration<Pessoa>
{
    public override void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.Telefone)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(11);

        base.Configure(builder);
    }
}
