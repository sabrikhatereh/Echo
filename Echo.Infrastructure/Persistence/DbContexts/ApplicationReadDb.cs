using Microsoft.EntityFrameworkCore;
using Echo.Application.Abstractions.DbContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using System.Linq;

namespace Echo.Infrastructure.Persistence.DbContexts
{
    public class ApplicationReadDb : IApplicationReadDb
    {
        private readonly DbContext db;

        public ApplicationReadDb(EchoWriteDbContext db)
        {
            this.db = db;
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            where T : class
            => await db.Set<T>().Where(predicate).AsNoTracking().ToListAsync();


        public async Task<T> QueryFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            where T : class
        => await db.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<T> QuerySingleAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class
         => await db.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);

        public async Task<IReadOnlyList<TResult>> QueryAsync<T, TResult>(Expression<Func<T, bool>> predicate,
                                                                         Expression<Func<T, TResult>> selector,
                                                                         CancellationToken cancellationToken = default) where T : class
        {
            return await db.Set<T>()
                .Where(predicate)
                .Select(selector)
                .ToListAsync(cancellationToken);
        }
    }

}

