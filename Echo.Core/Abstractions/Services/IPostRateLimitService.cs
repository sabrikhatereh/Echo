using System;
using System.Threading.Tasks;

namespace Echo.Core.Abstractions.Services
{
    public interface IPostRateLimitService
    {
        Task<bool> IsPostRateLimited(Guid UserId);
        public Task UpdateRateLimit(Guid UserId);
    }
}
