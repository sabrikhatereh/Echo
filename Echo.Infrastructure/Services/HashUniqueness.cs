using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Echo.Core;
using Echo.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;

namespace Echo.Infrastructure.Services
{
    public class HashUniqueness : IHashUniqueness
    {
        private readonly ILogger<HashUniqueness> _logger;
        private readonly IEasyCachingProvider _cachingProvider;
        private readonly IEchoQueryRepository _EchoQueryRepository;

        public HashUniqueness(ILogger<HashUniqueness> logger, IEasyCachingProvider cachingProvider,
            IEchoQueryRepository EchoQueryRepository)
        {
            _logger = logger;
            _cachingProvider = cachingProvider;
            _EchoQueryRepository = EchoQueryRepository;
        }

        public async Task<bool> IsUnique(Guid UserId, string hash)
        {
            var cacheKey = $"{UserId}__{hash}";
            var cachedResponse = await _cachingProvider.GetAsync<string>(cacheKey);
            if (cachedResponse.HasValue)
                return false;

            var Echo = await _EchoQueryRepository.Search(UserId, hash);
            if (Echo is null)
                return true;

            return false;

        }

        public async Task UpdateKeys(Guid UserId, string hash)
        {
            var cacheKey = $"{UserId}__{hash}";
            await _cachingProvider.SetAsync(cacheKey, "1", TimeSpan.MaxValue);
        }
    }
}
