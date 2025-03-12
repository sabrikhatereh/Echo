using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using Echo.Core.Abstractions.Models;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Echo.Core.Models;

namespace Echo.Application.Abstractions.DbContexts
{
    public interface IApplicationReadDb
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;

        Task<T> QueryFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate,  CancellationToken cancellationToken = default) where T : class;

        Task<T> QuerySingleAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;
        Task<IReadOnlyList<TResult>> QueryAsync<T, TResult>(Expression<Func<T, bool>> predicate,
           Expression<Func<T, TResult>> selector,
           CancellationToken cancellationToken = default) where T : class;
    }

}