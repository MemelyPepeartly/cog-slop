namespace CogSlop.Api.Models.Dtos;

public record MarketplaceListingDto(
    int MarketplaceListingId,
    int GearItemId,
    string GearName,
    string GearType,
    string? FlavorText,
    int Quantity,
    int PriceInCogs,
    int SellerUserAccountId,
    string SellerDisplayName,
    string ListingStatus,
    string? SellerNote,
    DateTime CreatedAtUtc,
    DateTime? SoldAtUtc);
