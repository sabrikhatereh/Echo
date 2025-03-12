using FluentValidation;
using Echo.Application.Validators;
using Echo.Core.Abstractions.Services;

namespace Echo.Application.Echos.Commands.AddEcho
{
    public class CreateEchoCommandValidator : AbstractValidator<CreateEchoCommand>
    {
        private readonly IForbidWords _forbidWords;
        public CreateEchoCommandValidator(IForbidWords forbidWords)
        {
            _forbidWords = forbidWords;
            var forbidWordsList = _forbidWords.LoadForbidWords().Result;
            RuleFor(x => x.Message)
            .NotNull()
            .NotEmpty()
            .WithMessage("Message is required")
            .MaximumLength(400)
            .WithMessage("Message cannot exceed 400 characters.")
            .MustNotContainForbiddenWords(forbidWordsList);

            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("UserId is required");

        }
    }
}
