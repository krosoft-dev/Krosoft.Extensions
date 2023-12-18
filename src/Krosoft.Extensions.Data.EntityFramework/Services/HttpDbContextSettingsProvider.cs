using Krosoft.Extensions.Core.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Krosoft.Extensions.Identity.Abstractions.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Services;

public class HttpDbContextSettingsProvider : IDbContextSettingsProvider
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IIdentityService _identityService;

    public HttpDbContextSettingsProvider(IIdentityService identityService,
                                         IDateTimeService dateTimeService)
    {
        _identityService = identityService;
        _dateTimeService = dateTimeService;
    }

    public string GetTenantId() => _identityService.GetTenantId();

    public DateTime GetNow() => _dateTimeService.Now;

    public string GetUtilisateurId() => _identityService.GetId();
}