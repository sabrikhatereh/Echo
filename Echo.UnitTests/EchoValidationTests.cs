using FluentValidation.TestHelper;
using Moq;
using Echo.Application.Echos.Commands.AddEcho;
using Echo.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Echo.UnitTests
{
    public class EchoValidationTests
    {
        [Fact]
        public void Validate_Should_Pass_For_Valid_Command()
        {
            // Arrange
            var mockForbidWords = new Mock<IForbidWords>();
            mockForbidWords.Setup(fw => fw.LoadForbidWords()).ReturnsAsync(new List<string> { "forbidden", "restricted" });

            var validator = new CreateEchoCommandValidator(mockForbidWords.Object);
            var command = new CreateEchoCommand
            {
                Message = "This is a valid message.",
                UserId = Guid.NewGuid()
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("This message contains a forbidden word Tweet.")]
        [InlineData("123Tweet")]
        [InlineData("abcTweeter")]
        [InlineData("abc_Tweeter")]
        public void Validate_Should_Fail_When_Message_Contains_Forbidden_Words(string message)
        {
            // Arrange
            var mockForbidWords = new Mock<IForbidWords>();
            mockForbidWords.Setup(fw => fw.LoadForbidWords()).ReturnsAsync(new List<string> { "Tweet", "Tweeter" });

            var validator = new CreateEchoCommandValidator(mockForbidWords.Object);
            var command = new CreateEchoCommand
            {
                Message = message,
                UserId = Guid.NewGuid()
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Message).WithErrorMessage("'Message' contains forbidden words.");
        }

        [Fact]
        public void Validate_Should_Fail_When_Message_Exceeds_Max_Length()
        {
            // Arrange
            var mockForbidWords = new Mock<IForbidWords>();
            mockForbidWords.Setup(fw => fw.LoadForbidWords()).ReturnsAsync(new List<string> { "forbidden", "restricted" });

            var validator = new CreateEchoCommandValidator(mockForbidWords.Object);

            var command = new CreateEchoCommand
            {
                Message = new string('a', 401), // 401 characters
                UserId = Guid.NewGuid()
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Message)
                  .WithErrorMessage("Message cannot exceed 400 characters.");
        }

        [Fact]
        public void Validate_Should_Fail_When_UserId_Is_Null()
        {
            // Arrange
            var mockForbidWords = new Mock<IForbidWords>();
            mockForbidWords.Setup(fw => fw.LoadForbidWords()).ReturnsAsync(new List<string> { "forbidden", "restricted" });

            var validator = new CreateEchoCommandValidator(mockForbidWords.Object);
            var command = new CreateEchoCommand
            {
                Message = "Valid message",
                UserId = Guid.Empty
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId).WithErrorMessage("UserId is required");
        }


    }

}
