using FluentValidation;
using System.Collections.Generic;

namespace Echo.Application.Validators
{
    public static class CustomValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> MustNotContainForbiddenWords<T>(
            this IRuleBuilder<T, string> ruleBuilder, IEnumerable<string> forbiddenWords)
        {
            return ruleBuilder.SetValidator(new ForbiddenWordsValidator<T>(forbiddenWords));
        }
    }
}
