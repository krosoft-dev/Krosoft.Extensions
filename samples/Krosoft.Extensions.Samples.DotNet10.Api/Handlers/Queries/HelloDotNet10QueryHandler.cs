using Krosoft.Extensions.Samples.Library.Models.Queries;
using MediatR;

namespace Krosoft.Extensions.Samples.DotNet10.Api.Handlers.Queries;

public class HelloDotNet10QueryHandler : IRequestHandler<HelloDotNet10Query, string>
{
    private readonly ILogger<HelloDotNet10QueryHandler> _logger;

    public HelloDotNet10QueryHandler(ILogger<HelloDotNet10QueryHandler> logger)
    {
        _logger = logger;
    }

    public Task<string> Handle(HelloDotNet10Query request,
                               CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello DotNet10...");

        return Task.FromResult("Hello DotNet10");
    }
}