using MadaDaPesca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MadaDaPesca.Infra.Extensions;

internal static class PaginacaoQuerableExtension
{
    public static IQueryable<TEntity> SkipAndTake<TEntity>(this IQueryable<TEntity> querable, int skip, int take)
    {
        var newSkip = skip - 1;
        if (newSkip < 0)
        {
            newSkip = 0;
        }
        return querable
            .Skip(newSkip * take)
            .Take(take);
    }

    public static async Task<int> TotalPages<TEntity>(this IQueryable<TEntity> queryable, int take)
    {
        var count = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)count / take);
        return totalPages;
    }

    public static IQueryable<TEntity> WhereIfNotNull<TEntity>(this IQueryable<TEntity> querable, Expression<Func<TEntity, bool>>? where)
    {
        if (where == null)
            return querable;

        return querable.Where(where);
    }

    public static async Task<IList<TEntity>> ListarAsync<TEntity>(this IQueryable<TEntity> querable, FilterModel<TEntity> filterModel)
    {
        var coluna = filterModel.OrderBy[..1].ToUpper() + filterModel.OrderBy[1..];

        Expression<Func<TEntity, object>> orderBy = x => EF.Property<TEntity>(x!, coluna)!;

        querable = filterModel.Asc ? querable.OrderBy(orderBy) : querable.OrderByDescending(orderBy);
        var values = await querable
            .SkipAndTake(filterModel.Skip, filterModel.Take)
            .ToListAsync();

        return values;
    }
}
