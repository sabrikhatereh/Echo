using Echo.Core.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Echo.Core
{

    public interface IEchoQueryRepository
    {
        Task<IReadOnlyList<EchoEntity>> GetByUserId(Guid userId);
        Task<IReadOnlyList<string>> GetHashByUserId(Guid userId);
        Task<EchoEntity> Search(Guid userId, string hashId);
        Task<EchoEntity> Get(Guid Id);

    }

}