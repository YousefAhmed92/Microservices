using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request}" +
                " - Response={Response}"
                , typeof(TRequest).Name
                , typeof(TResponse).Name);

            var timer = new Stopwatch();

            var response = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;

            if(timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] {RequestType} took {TimeTaken} seconds, which is longer than expected.", typeof(TRequest).Name, timeTaken.TotalSeconds);
            }

            logger.LogInformation("[END] Handle request={Request}" +
                " - Response={Response}"
                , typeof(TRequest).Name
                , typeof(TResponse).Name);
            
            return response;
        }
    }
}
