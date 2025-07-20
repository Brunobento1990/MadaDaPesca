using MadaDaPesca.Domain.Entities;
using MadaDaPesca.Infra.EntitieConfiguration;
using Microsoft.EntityFrameworkCore;

namespace MadaDaPesca.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AcessoGuiaDePesca> AcessosGuiasDePesca { get; set; }
    public DbSet<GuiaDePesca> GuiasDePesca { get; set; }
    public DbSet<Pescaria> Pescarias { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AcessoGuiaDePescaConfiguration());
        modelBuilder.ApplyConfiguration(new GuiaDePescaConfiguration());
        modelBuilder.ApplyConfiguration(new PescariaConfiguration());
        modelBuilder.ApplyConfiguration(new PessoaConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
