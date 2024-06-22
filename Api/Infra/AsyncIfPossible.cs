using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Api.Infra
{
    public static class AsyncIfPossible
    {
        public static async Task<T?> FirstOrDefaultTryAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            if (query is IAsyncQueryProvider)
            {
                return await query.FirstOrDefaultAsync(expression, cancellationToken);
            }
            else
            {
                return query.FirstOrDefault(expression);
            }
        }

        public static async Task<T?> FirstOrDefaultTryAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            if (query is IAsyncQueryProvider)
            {
                return await query.FirstOrDefaultAsync(cancellationToken);
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public static async Task<List<T>> ToListTryAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            if (query is IAsyncQueryProvider)
            {
                return await query.ToListAsync(cancellationToken);
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
