using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class GrantGearRequest
{
    [Range(1, int.MaxValue)]
    public int UserAccountId { get; set; }

    [Range(1, int.MaxValue)]
    public int GearItemId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; } = 1;

    [MaxLength(300)]
    public string Note { get; set; } = "Manual gear injection";
}
