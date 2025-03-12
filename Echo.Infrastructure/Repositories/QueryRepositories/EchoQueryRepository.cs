using Echo.Application.Abstractions.DbContexts;
using Echo.Core;
using Echo.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Echo.Infrastructure.Repositories.QueryRepositories
{
    public class EchoQueryRepository : IEchoQueryRepository
    {
        private readonly IApplicationReadDb db;
        public EchoQueryRepository(IApplicationReadDb db)
        {
            this.db = db;
        }



        public async Task<EchoEntity> Get(Guid Id)
        => await db.QueryFirstOrDefaultAsync<EchoEntity>(x => x.Id == Id);

        public async Task<IReadOnlyList<EchoEntity>> GetByUserId(Guid userId)
        => await db.QueryAsync<EchoEntity>(x => x.UserId == userId);


        public async Task<IReadOnlyList<string>> GetHashByUserId(Guid userId)
            => await db.QueryAsync<EchoEntity, string>(x => x.UserId == userId,
                e => e.Hash);

        public async Task<EchoEntity> Search(Guid userId, string hashId)
       => await db.QueryFirstOrDefaultAsync<EchoEntity>(x => x.UserId == userId && x.Hash == hashId);
        //await db.Echos.FirstOrDefaultAsync(x => x.UserId == userId && x.Hash == hashId);
        
    }

}
