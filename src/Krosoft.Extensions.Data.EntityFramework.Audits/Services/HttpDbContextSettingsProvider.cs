using Krosoft.Extensions.Core.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Audits.Services;

public class HttpDbContextSettingsProvider2 : IDbContextSettingsProvider2
{
    private readonly IDateTimeService _dateTimeService; 

    public HttpDbContextSettingsProvider2( 
                                         IDateTimeService dateTimeService)
    {
        
        _dateTimeService = dateTimeService;
    }

     

    public DateTime GetNow() => _dateTimeService.Now;
     
}