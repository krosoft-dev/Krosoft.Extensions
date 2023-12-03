namespace Krosoft.Extensions.WebApi.HealthChecks.Models;

public class HealthCheckStatusModel
{
    public string Status { get; set; }
    public IEnumerable<HealthCheckModel> Checks { get; set; }
    public string Duration { get; set; }
    public string Environnement { get; set; }
}