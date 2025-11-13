namespace Krosoft.Extensions.Samples.DotNet10.Api.Models;

internal record AppSettings
{
    public string BaseUrl { get; set; } = null!;
    public string Token { get; set; } = null!;

    public IEnumerable<JobAmqpSettings> JobsAmqp { get; set; } = new List<JobAmqpSettings>();
}