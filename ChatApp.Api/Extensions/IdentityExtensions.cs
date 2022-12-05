using System.Security.Claims;
using System.Security.Principal;

namespace ChatApp.Api.Extensions;

public static class IdentityExtensions
{
    public static Guid GetUserId(this IIdentity identity)
    {
        ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
        Claim claim = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == "userId");
        Guid userId = Guid.Empty;
        Guid.TryParse(claim?.Value, out userId);
        return userId;
    }
}