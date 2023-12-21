using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Samples.Library.Models.Entities;
using Krosoft.Extensions.Samples.Library.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Handlers.Queries;

public class HelloDatabaseQueryHandler : IRequestHandler<HelloDatabaseQuery, string>
{
    private readonly ILogger<HelloDatabaseQueryHandler> _logger;
    private readonly IReadRepository<Langue> _repository;

    public HelloDatabaseQueryHandler(ILogger<HelloDatabaseQueryHandler> logger, IReadRepository<Langue> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<string> Handle(HelloDatabaseQuery request,
                                     CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello DotNet8...");

        var logiciels = await _repository.Query().ToListAsync(cancellationToken);
        return "Hello " + logiciels.Count;
    }
}