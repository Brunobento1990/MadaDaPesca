using MadaDaPesca.Domain.Models;

namespace MadaDaPesca.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    void Editar(T entity);
    Task SaveChangesAsync();
    Task<PaginacaoModel<T>> PaginacaoAsync(FilterModel<T> filterModel);
}
