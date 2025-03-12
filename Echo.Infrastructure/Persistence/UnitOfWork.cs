using Echo.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Echo.Infrastructure.Persistence.DbContexts;
using Microsoft.Extensions.Logging;

namespace Echo.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EchoWriteDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(EchoWriteDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while saving changes to the database");
                throw;
            }
        }
    }
}
