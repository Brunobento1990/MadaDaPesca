using MadaDaPesca.Domain.Interfaces;
using MadaDaPesca.Domain.Models;
using MadaDaPesca.Infra.Context;
using MadaDaPesca.Infra.Extensions;
using Microsoft.EntityFrameworkCore;

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

    public void Editar(T entity)
    {
        AppDbContext.Set<T>().Update(entity);
    }

    public async Task<PaginacaoModel<T>> PaginacaoAsync(FilterModel<T> filterModel)
    {
        var query = AppDbContext
            .Set<T>()
            .AsNoTracking()
            .WhereIfNotNull(filterModel.Where());

        var include = filterModel.Include();

        if (include != null)
        {
            query = query.Include(include);
        }

        var values = await query
            .ListarAsync(filterModel);

        var totalDePaginas = await AppDbContext.Set<T>().WhereIfNotNull(filterModel.Where()).TotalPages(filterModel.Take);

        return new()
        {
            Lista = values,
            QuantidadeDePaginas = totalDePaginas
        };
    }

    public async Task SaveChangesAsync()
    {
        await AppDbContext.SaveChangesAsync();
    }
}
