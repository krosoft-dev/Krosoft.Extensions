using Krosoft.Extensions.Samples.Library.Models.Dto;
using Krosoft.Extensions.Samples.Library.Models.Queries;
using Krosoft.Extensions.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Krosoft.Extensions.Samples.DotNet10.Api.Controllers;

public class SettingsController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("CorsPolicy")]
    public Task<CorsPolicyDto> CorsPolicyAsync(CancellationToken cancellationToken)
        => Mediator.Send(new CorsPolicyQuery(), cancellationToken);
}