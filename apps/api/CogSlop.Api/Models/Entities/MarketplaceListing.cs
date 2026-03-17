namespace CogSlop.Api.Models.Entities;

public class MarketplaceListing
{
    public int MarketplaceListingId { get; set; }

    public int SellerUserAccountId { get; set; }

    public int? BuyerUserAccountId { get; set; }

    public int GearItemId { get; set; }

    public int Quantity { get; set; }

    public int PriceInCogs { get; set; }

    public string ListingStatus { get; set; } = string.Empty;

    public string? SellerNote { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? SoldAtUtc { get; set; }

    public UserAccount SellerUserAccount { get; set; } = null!;

    public UserAccount? BuyerUserAccount { get; set; }

    public GearItem GearItem { get; set; } = null!;
}
