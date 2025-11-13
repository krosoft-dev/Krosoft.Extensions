using Krosoft.Extensions.Samples.Library.Models.Queries;

namespace Krosoft.Extensions.Samples.DotNet7.Api.Handlers.Queries;

public class HelloDotNet7QueryHandler : IRequestHandler<HelloDotNet7Query, string>
{
    private readonly ILogger<HelloDotNet7QueryHandler> _logger;

    public HelloDotNet7QueryHandler(ILogger<HelloDotNet7QueryHandler> logger)
    {
        _logger = logger;
    }

    public Task<string> Handle(HelloDotNet7Query request,
                               CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello DotNet7...");

        return Task.FromResult("Hello DotNet7");
    }
}