using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Echo.Core.Events;
using Echo.Core.Abstractions.Events;
using EasyCaching.Core;
using Echo.Core.Abstractions.Services;

namespace Echo.Application.Echos.Commands.AddEcho
{
    public class EchoCreatedEventHandler : IEventHandler<EchoCreatedDomainEvent>
    {
        private readonly ILogger<EchoCreatedEventHandler> logger;
        private readonly IPostRateLimitService _rateLimitService;
        private readonly IHashUniqueness _hashUniqueness;

        public EchoCreatedEventHandler(ILogger<EchoCreatedEventHandler> logger, IPostRateLimitService rateLimitService,
            IHashUniqueness hashUniqueness)
        {
            this.logger = logger;
            _rateLimitService = rateLimitService;
            _hashUniqueness = hashUniqueness;
        }

        public async Task Handle(EchoCreatedDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            logger.LogInformation($"UserId [{domainEvent.UserId}, created  Echo {domainEvent.EchoDate}] .");
            await Task.WhenAll(
            _rateLimitService.UpdateRateLimit(domainEvent.UserId),
            _hashUniqueness.IsUnique(domainEvent.UserId, domainEvent.Hash));
        }
    }
}
