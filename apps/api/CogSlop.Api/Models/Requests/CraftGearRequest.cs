using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class CraftGearRequest
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(600)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(60)]
    public string GearType { get; set; } = "Custom";

    [Range(1, 1000000)]
    public int CraftingCostInCogs { get; set; } = 25;

    [MaxLength(300)]
    public string? FlavorText { get; set; }
}
