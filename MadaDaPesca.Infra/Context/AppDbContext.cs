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
    public DbSet<AgendaPescaria> AgendaPescarias { get; set; }
    public DbSet<GaleriaAgendaPescaria> GaleriaAgendaPescaria { get; set; }
    public DbSet<GaleriaFotoEmbarcacao> GaleriaFotoEmbarcacoes { get; set; }
    public DbSet<Embarcacao> Embarcacoes { get; set; }
    public DbSet<BloqueioDataPescaria> DatasBloqueadas { get; set; }
    public DbSet<FaturaAgendaPescaria> FaturasAgendaPescarias { get; set; }
    public DbSet<TransacaoFaturaAgendaPescaria> TransacoesFaturaAgenda { get; set; }
    public DbSet<GaleriaDeTrofeu> GaleriaDeTrofeus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FaturaAgendaPescariaConfiguration());
        modelBuilder.ApplyConfiguration(new TransacaoFaturaAgendaPescariaConfiguration());
        modelBuilder.ApplyConfiguration(new GaleriaFotoEmbarcacaoConfiguration());
        modelBuilder.ApplyConfiguration(new EmbarcacaoConfiguration());
        modelBuilder.ApplyConfiguration(new GaleriaAgendaPescariaConfiguration());
        modelBuilder.ApplyConfiguration(new AcessoGuiaDePescaConfiguration());
        modelBuilder.ApplyConfiguration(new GuiaDePescaConfiguration());
        modelBuilder.ApplyConfiguration(new PescariaConfiguration());
        modelBuilder.ApplyConfiguration(new PessoaConfiguration());
        modelBuilder.ApplyConfiguration(new AgendaPescariConfiguration());
        modelBuilder.ApplyConfiguration(new GaleriaDeTrofeuConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
