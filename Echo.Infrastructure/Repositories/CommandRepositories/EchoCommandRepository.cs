using Echo.Core;
using Echo.Core.Models;
using Echo.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Infrastructure.Repositories.CommandRepositories
{
    public class EchoCommandRepository : IEchoCommandRepository
    {
        private readonly EchoWriteDbContext db;

        public EchoCommandRepository(EchoWriteDbContext db)
        {
            this.db = db;
        }

        public async Task Add(EchoEntity Echo)
        {
           await db.Echos.AddAsync(Echo);
        }

        public Task Delete(EchoEntity Echo)
        {
            throw new NotImplementedException();
        }

        public Task Update(EchoEntity Echo)
        {
            throw new NotImplementedException();
        }
    }
}
