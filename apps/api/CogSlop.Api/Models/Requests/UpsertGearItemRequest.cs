using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class UpsertGearItemRequest
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(600)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(60)]
    public string GearType { get; set; } = "Gadget";

    [Range(0, 1000000)]
    public int CostInCogs { get; set; }

    [Range(0, 100000)]
    public int? StockQuantity { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(300)]
    public string? FlavorText { get; set; }
}
