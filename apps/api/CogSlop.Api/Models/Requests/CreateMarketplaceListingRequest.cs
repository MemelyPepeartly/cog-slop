using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class CreateMarketplaceListingRequest
{
    [Range(1, int.MaxValue)]
    public int GearItemId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;

    [Range(1, 1000000)]
    public int PriceInCogs { get; set; }

    [MaxLength(300)]
    public string? SellerNote { get; set; }
}
