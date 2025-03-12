using AutoMapper;
using MediatR;
using Moq;
using Echo.Application.Exceptions;
using Echo.Application.Echos.Commands.AddEcho;
using Echo.Core.Abstractions;
using Echo.Core;
using Echo.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Echo.Application;

namespace Echo.UnitTests
{
    public class CreateEchoCommandHandlerTests
    {
        private readonly Mock<IPostRateLimitService> _rateLimitServiceMock;
        private readonly Mock<IHashUniqueness> _hashUniquenessMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEchoCommandRepository> _commandRepositoryMock;

        private readonly CreateEchoCommandHandler _handler;

        public CreateEchoCommandHandlerTests()
        {
            _rateLimitServiceMock = new Mock<IPostRateLimitService>();
            _hashUniquenessMock = new Mock<IHashUniqueness>();
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _commandRepositoryMock = new Mock<IEchoCommandRepository>();

            _handler = new CreateEchoCommandHandler(
                _commandRepositoryMock.Object,
                _hashUniquenessMock.Object,
                _rateLimitServiceMock.Object,
                _mediatorMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldThrowRateLimitException_WhenRateLimitExceeded()
        {
            // Arrange
            var command = new CreateEchoCommand
            {
                UserId = Guid.NewGuid(),
                Message = "Test message"
            };

            _rateLimitServiceMock
                .Setup(s => s.IsPostRateLimited(command.UserId))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<PostRateLimitException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowDuplicatedRequestException_WhenHashIsNotUnique()
        {
            // Arrange
            var command = new CreateEchoCommand
            {
                UserId = Guid.NewGuid(),
                Message = "Test message"
            };

            var hash = command.Message.GenerateSHA256Hash();

            _rateLimitServiceMock
                .Setup(s => s.IsPostRateLimited(command.UserId))
                .ReturnsAsync(false);

            _hashUniquenessMock
                .Setup(h => h.IsUnique(command.UserId, hash))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<DuplicatedRequestException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }


}
