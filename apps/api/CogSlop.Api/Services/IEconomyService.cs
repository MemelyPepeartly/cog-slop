using System.Security.Claims;
using CogSlop.Api.Models.Dtos;

namespace CogSlop.Api.Services;

public interface IEconomyService
{
    Task<DashboardDto> GetDashboardAsync(ClaimsPrincipal principal, CancellationToken cancellationToken);

    Task<PurchaseReceiptDto> BuyGearAsync(ClaimsPrincipal principal, int gearItemId, int quantity, CancellationToken cancellationToken);

    Task<IReadOnlyList<StoreItemDto>> GetStoreItemsAsync(bool includeInactive, CancellationToken cancellationToken);
}
