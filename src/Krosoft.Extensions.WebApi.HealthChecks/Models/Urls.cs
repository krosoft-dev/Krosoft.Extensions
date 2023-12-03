namespace Krosoft.Extensions.WebApi.HealthChecks.Models;

public static class Urls
{
    public static class Health
    {
        public const string Check = "/Health/Check";
        public const string Readiness = "/Health/Readiness";
        public const string Liveness = "/Health/Liveness";
    }
}