using System.Security.Claims;

namespace CogSlop.Api.Services;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    public ExternalUserIdentity GetRequiredIdentity(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        var googleSubject = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? principal.FindFirst("sub")?.Value;
        var email = principal.FindFirst(ClaimTypes.Email)?.Value
            ?? principal.FindFirst("email")?.Value;

        if (string.IsNullOrWhiteSpace(googleSubject) || string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidOperationException("Google identity claims are missing.");
        }

        var displayName = principal.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrWhiteSpace(displayName))
        {
            displayName = email.Split('@')[0];
        }

        var avatarUrl = principal.FindFirst("urn:google:picture")?.Value
            ?? principal.FindFirst("picture")?.Value;

        return new ExternalUserIdentity(
            googleSubject,
            email,
            displayName,
            avatarUrl);
    }
}
