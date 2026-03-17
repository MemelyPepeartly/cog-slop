using System.Security.Claims;

namespace CogSlop.Api.Services;

public interface ICurrentUserAccessor
{
    ExternalUserIdentity GetRequiredIdentity(ClaimsPrincipal principal);
}
