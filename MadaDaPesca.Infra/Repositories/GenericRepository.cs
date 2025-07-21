using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Infra.Context;

namespace MadaDaPesca.Infra.Repositories;

internal class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext AppDbContext;

    public GenericRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    public async Task AddAsync(T entity)
    {
        await AppDbContext.Set<T>().AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await AppDbContext.SaveChangesAsync();
    }
}
