using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Web.Pipelines
{
    public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest :IRequest<TResponse>
    {
        private readonly ILogger _logger;
        public LoggingPipeline(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.Information($"Request received at {System.DateTime.UtcNow}");
            var response = await next();
            _logger.Information("{@request} {@response}", request, response);
            return response;
        }
    }
}
