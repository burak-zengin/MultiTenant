using Microsoft.AspNetCore.Http;

namespace Shared;

public class TenantProvider(IHttpContextAccessor httpContextAccessor)
{
    public Guid GetTenantId()
    {
        var value = httpContextAccessor?.HttpContext?.User.Claims.First(c => c.Type == "id").Value;

        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("Invalid tenant id.");
        }

        return Guid.Parse(value);
    }
}
