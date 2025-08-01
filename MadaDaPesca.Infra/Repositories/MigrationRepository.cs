using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MadaDaPesca.Infra.Repositories;

internal class MigrationRepository : IMigrationRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;

    public MigrationRepository(AppDbContext appDbContext, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _configuration = configuration;
    }

    public async Task RodarMigrationAsync()
    {
        if (_configuration["RodarMigration"]?.ToLower() == "false")
        {
            return;
        }

        await _appDbContext.Database.MigrateAsync();
    }
}
