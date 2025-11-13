using Microsoft.AspNetCore.Mvc;

namespace Krosoft.Extensions.Samples.DotNet10.Api.Models.Dto;

internal record DeposerFichierDto(
    [FromForm] long FichierId,
    IFormFile File);