using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Echo.Core.Abstractions.Services;
using Echo.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace Echo.Infrastructure.Services
{
    public class PostRateLimitService : IPostRateLimitService
    {
        private readonly IEasyCachingProvider _cachingProvider;
        private readonly PostLimitSettings _rateLimitSettings;

       
        public PostRateLimitService(IOptions<PostLimitSettings> rateLimitSettings,
            IEasyCachingProvider cachingProvider)
        {
            _cachingProvider = cachingProvider;
            _rateLimitSettings = rateLimitSettings.Value;
        }

        public async Task<bool> IsPostRateLimited(Guid UserId)
        {
            var cacheKey = $"{UserId}__RateLimit";
            var cachedResponse = await _cachingProvider.GetAsync<string>(cacheKey);
            if (cachedResponse.HasValue)
                return true;

            return false;
        }

        public async Task UpdateRateLimit(Guid UserId)
        {
            var cacheKey = $"{UserId}__RateLimit";
            await _cachingProvider.SetAsync(cacheKey, "1", 
                TimeSpan.FromSeconds(_rateLimitSettings.EchosPerSeconds));
        }
    }
}
