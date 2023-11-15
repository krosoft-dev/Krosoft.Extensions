using Krosoft.Extensions.Core.Models.Dto;
using Krosoft.Extensions.Reporting.Csv.Extensions;
using Krosoft.Extensions.Samples.DotNet6.Api.Models.Commands;
using Krosoft.Extensions.Samples.DotNet6.Api.Models.Dto;
using Krosoft.Extensions.Samples.DotNet6.Api.Models.Queries;
using Krosoft.Extensions.WebApi.Controllers;
using Krosoft.Extensions.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Krosoft.Extensions.Samples.DotNet6.Api.Controllers;

public class LogicielsController : ApiControllerBase
{
    [HttpGet]
    public Task<IEnumerable<LogicielDto>> GetAsync(LogicielsQuery query,
                                                   CancellationToken cancellationToken)
        => Mediator.Send(query, cancellationToken);

    [HttpGet("PickList")]
    public Task<IEnumerable<PickListDto>> GetPickListAsync(CancellationToken cancellationToken)
        => Mediator.Send(new LogicielsPickListQuery(), cancellationToken);

    [HttpGet("{id:guid}")]
    public Task<LogicielDetailDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => Mediator.Send(new LogicielDetailQuery(id), cancellationToken);

    [HttpPut]
    public Task UpdateAsync([FromBody] LogicielUpdateCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [HttpPost]
    public Task<Guid> CreateAsync([FromBody] LogicielCreateCommand command, CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    [HttpPost("Import")]
    public async Task<int> ImportAsync(CancellationToken cancellationToken)
    {
        var files = await this.GetRequestToBase64StringAsync();
        return await Mediator.Send(new LogicielImportCommand(files), cancellationToken);
    }

    [HttpGet("Export")]
    public Task<FileStreamResult> ExportAsync(CancellationToken cancellationToken)
        => Mediator.Send(new LogicielsExportQuery(), cancellationToken)
                   .ToCsvStreamResult()
                   .ToFileStreamResult();

    [HttpDelete]
    public Task DeleteAsync([FromBody] LogicielsDeleteCommand command,
                            CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);
}