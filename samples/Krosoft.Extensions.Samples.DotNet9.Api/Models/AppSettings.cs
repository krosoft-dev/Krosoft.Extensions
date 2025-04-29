namespace Krosoft.Extensions.Samples.DotNet9.Api.Models;

internal record AppSettings
{
    public string BaseUrl { get; set; } = null!;
    public string Token { get; set; } = null!;
}