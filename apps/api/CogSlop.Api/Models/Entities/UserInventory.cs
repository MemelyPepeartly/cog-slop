namespace CogSlop.Api.Models.Entities;

public class UserInventory
{
    public int UserInventoryId { get; set; }

    public int UserAccountId { get; set; }

    public int GearItemId { get; set; }

    public int Quantity { get; set; }

    public DateTime LastGrantedAtUtc { get; set; }

    public UserAccount UserAccount { get; set; } = null!;

    public GearItem GearItem { get; set; } = null!;
}
