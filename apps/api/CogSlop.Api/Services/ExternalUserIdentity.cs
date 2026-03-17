namespace CogSlop.Api.Services;

public record ExternalUserIdentity(
    string GoogleSubject,
    string Email,
    string DisplayName,
    string? AvatarUrl);
