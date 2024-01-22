using Krosoft.Extensions.Blocking.Abstractions.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Services;

public class FakeAccessTokenProvider : IAccessTokenProvider
{
    public Task<string> GetAccessTokenAsync(CancellationToken cancellationToken) => Task.FromResult("Hello");
    public string GetTenantId() => "Fake_Tenant_Id";
}