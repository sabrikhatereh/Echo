using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Echo.Core.Abstractions.Services;
using Echo.Core;
using Echo.Core.Models;
using Echo.Application.Exceptions;
using AutoMapper;
using Echo.Core.Abstractions;

namespace Echo.Application.Echos.Commands.AddEcho
{
    public class CreateEchoCommandHandler : IRequestHandler<CreateEchoCommand, EchoViewModel?>
    {
        private readonly IEchoCommandRepository _EchoCommandRepository;
        private readonly IPostRateLimitService _rateLimitService;
        private readonly IHashUniqueness _hashUniquenessChecker;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public CreateEchoCommandHandler(IEchoCommandRepository EchoCommandRepository,
            IHashUniqueness hashUniquenessChecker,
            IPostRateLimitService rateLimitService,
            IMediator mediator,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _EchoCommandRepository = EchoCommandRepository;
            _hashUniquenessChecker = hashUniquenessChecker;
            _mediator= mediator;
            _mapper = mapper;
            _rateLimitService = rateLimitService;
            _unitOfWork = unitOfWork;
        }

        public async Task<EchoViewModel?> Handle(CreateEchoCommand command, CancellationToken cancellationToken)
        {
            if (await _rateLimitService.IsPostRateLimited(command.UserId))
                throw new PostRateLimitException("Rate limit exceeded");

            var hash = command.Message.GenerateSHA256Hash();

            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Hash cannot be null or whitespace.", nameof(hash));

            if (!await _hashUniquenessChecker.IsUnique(command.UserId, hash))
                throw new DuplicatedRequestException("Duplicated Echo");

            var Echo = EchoEntity.Create(command.UserId, command.Message, hash);
            await _EchoCommandRepository.Add(Echo);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in Echo.GetDomainEvents())
                await _mediator.Publish(domainEvent, cancellationToken);

            // Clear domain events after publishing
            Echo.ClearDomainEvents();

            return _mapper.Map<EchoEntity, EchoViewModel>(Echo);
        }
    }
}
