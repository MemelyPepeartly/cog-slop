namespace CogSlop.Api.Models.Entities;

public class GearItem
{
    public int GearItemId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string GearType { get; set; } = string.Empty;

    public int CostInCogs { get; set; }

    public int? StockQuantity { get; set; }

    public bool IsActive { get; set; }

    public string? FlavorText { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();

    public ICollection<CogTransaction> CogTransactions { get; set; } = new List<CogTransaction>();
}
