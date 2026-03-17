using System.Globalization;
using System.Security.Claims;
using CogSlop.Api.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CogSlop.Api.Services;

public class CogClaimsTransformation(
    CogSlopDbContext dbContext,
    ICurrentUserAccessor currentUserAccessor) : IClaimsTransformation
{
    private const string ClaimIssuer = "CogClaimsTransformation";

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;
        if (identity is null || identity.IsAuthenticated != true)
        {
            return principal;
        }

        if (identity.Claims.Any(x => x.Issuer == ClaimIssuer))
        {
            return principal;
        }

        ExternalUserIdentity externalIdentity;
        try
        {
            externalIdentity = currentUserAccessor.GetRequiredIdentity(principal);
        }
        catch (InvalidOperationException)
        {
            return principal;
        }

        var user = await dbContext.UserAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.GoogleSubject == externalIdentity.GoogleSubject || x.Email == externalIdentity.Email);

        if (user is null)
        {
            return principal;
        }

        var roleNames = await dbContext.UserRoles
            .AsNoTracking()
            .Where(x => x.UserAccountId == user.UserAccountId)
            .Select(x => x.Role.Name)
            .ToListAsync();

        var transformedIdentity = new ClaimsIdentity(identity);

        transformedIdentity.AddClaim(new Claim(
            CogClaimTypes.UserAccountId,
            user.UserAccountId.ToString(CultureInfo.InvariantCulture),
            ClaimValueTypes.Integer32,
            ClaimIssuer));

        foreach (var roleName in roleNames)
        {
            transformedIdentity.AddClaim(new Claim(
                ClaimTypes.Role,
                roleName,
                ClaimValueTypes.String,
                ClaimIssuer));
        }

        return new ClaimsPrincipal(transformedIdentity);
    }
}
