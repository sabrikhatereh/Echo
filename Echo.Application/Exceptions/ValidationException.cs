
using Echo.Application.Behaviors;
using Echo.Core.Shared.Result;

namespace Echo.Application.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException(ValidationResultModel validationResultModel)
        {
            ValidationResultModel = validationResultModel;
        }
        public ValidationResultModel ValidationResultModel { get; }
    }
}

