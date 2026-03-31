using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext Context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message {exceptionMessage} at {time}", exception.Message, DateTime.UtcNow);

            (string Details, string Title, int StatusCode) Details = exception switch
            {
                InternalServerException =>
                (exception.Message,
                "Internal Server Error",
                StatusCodes.Status500InternalServerError
                ),

                BadRequestException =>
                (
                exception.Message,
                "Bad Request",
                StatusCodes.Status400BadRequest
                ),

                ValidationException =>
                (
                exception.Message,
                "Validation Error",
                StatusCodes.Status400BadRequest
                ),

                NotFoundException =>
                (
                exception.Message,
                "Not Found",
                StatusCodes.Status404NotFound
                ),

                _ =>
                (
                exception.Message,
                "An error occurred",
                StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = Details.Title,
                Detail = Details.Details,
                Status = Details.StatusCode,
                Instance = Context.Request.Path
            };

            problemDetails.Extensions.Add("Trace Id", Context.TraceIdentifier);

            if(exception is ValidationException validation)
            {
                problemDetails.Extensions.Add("Errors", validation.Errors);
            }

            await Context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
