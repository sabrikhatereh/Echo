using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Echo.Application.Abstractions.DbContexts;
using System;
using System.Threading.Tasks;
using System.Threading;
using Echo.Core.Models;
using Echo.Core.Abstractions.Models;
using System.Linq;
using Echo.Application.Exceptions;

namespace Echo.Infrastructure.Persistence.DbContexts
{
    public class EchoWriteDbContext : DbContext, IApplicationWriteDbContext
    {
        //private readonly ICurrentUserService currentUserService = default!;

        //public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<EchoEntity> Echos { get; set; } = default!;

        public bool HasChanges => ChangeTracker.HasChanges();
        public EchoWriteDbContext(DbContextOptions<EchoWriteDbContext> options)
                                                    : base(options)
        {
            // this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = currentUserService.UserId;
                        entry.Entity.CreatedOnUtc = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        // entry.Entity.LastModifiedBy = currentUserService.UserId;
                        entry.Entity.ModifiedOnUtc = DateTime.UtcNow;
                        break;
                }
            }
            EnforceUniqueConstraints();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EchoEntity>()
                .HasIndex(m => new { m.Hash, m.UserId })
                .IsUnique();
        }

        // enforcement of unique constraints manually as In-Memory Database provider does not enforce Unique indexes
        private void EnforceUniqueConstraints()
        {
            var Echos = this.Set<EchoEntity>().AsNoTracking().ToList();

            var duplicate = ChangeTracker.Entries<EchoEntity>()
                .GroupBy(s => new { s.Entity.Hash, s.Entity.UserId })
                .Where(g => g.Count() > 1)
                .Select(g => g.First())
                .FirstOrDefault();

            if (duplicate != null)
            {
                throw new ConflictException("Unique constraint violation: Hash and UserId must be unique.");
            }
        }
    }
}
