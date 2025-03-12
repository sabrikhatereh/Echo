using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Echo.Application.Exceptions;
using System;
using Newtonsoft.Json;


namespace Echo.Infrastructure.ErrorHandling
{
    public static class CustomErrorHandlingMiddleware
    {
        public static IServiceCollection AddCustomErrorHandlingMiddleware(this IServiceCollection services)
        {
            services.AddProblemDetails(x =>
            {
                // Control when an exception is included
                x.IncludeExceptionDetails = (ctx, _) =>
                {
                    //return false;
                    var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                    return env.IsDevelopment() || env.IsStaging();
                };
                x.ShouldLogUnhandledException = (context, exception, problemDetails) =>
                {

                    if (exception is ValidationException)
                    {
                        // Skip logging for specific exception types
                        return false;
                    }

                    // Log all other exceptions
                    return true;
                };
                
                x.Map<ConflictException>(ex => new ProblemDetails
                {
                    Title = "Application rule broken",
                    Status = StatusCodes.Status409Conflict,
                    Detail = ex.Message,
                    Type = "https://somedomain/application-rule-validation-error",
                });

                // Exception will produce and returns from our FluentValidation RequestValidationBehavior
                x.Map<ValidationException>(ex => new ProblemDetails
                {
                    Title = "input validation rules broken",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonConvert.SerializeObject(ex.ValidationResultModel.Errors),
                    Type = "https://somedomain/input-validation-rules-error"
                });
                x.Map<BadRequestException>(ex => new ProblemDetails
                {
                    Title = "bad request exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/bad-request-error"
                });
                x.Map<DuplicatedRequestException>(ex => new ProblemDetails
                {
                    Title = "duplicated request exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/bad-request-error"
                });
                x.Map<PostRateLimitException>(ex => new ProblemDetails
                {
                    Title = "too many post requests",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/bad-request-error"
                });
                x.Map<NotFoundException>(ex => new ProblemDetails
                {
                    Title = "not found exception",
                    Status = StatusCodes.Status404NotFound,
                    Detail = ex.Message,
                    Type = "https://somedomain/not-found-error"
                });
                x.Map<InternalServerException>(ex => new ProblemDetails
                {
                    Title = "api server exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/api-server-error"
                });
                x.Map<AppException>(ex => new ProblemDetails
                {
                    Title = "application exception",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Type = "https://somedomain/application-error"
                });

                x.Map<ArgumentNullException>(ex => new ProblemDetails
                {
                    Title = "bad request exception",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = ex.Message,
                    Type = "https://somedomain/bad-request-error"
                });

               
            });
            return services;
        }
    }
}
