using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class GrantCogsRequest
{
    [Range(1, int.MaxValue)]
    public int UserAccountId { get; set; }

    [Range(1, 100000)]
    public int Amount { get; set; }

    [MaxLength(300)]
    public string Note { get; set; } = "Admin crank bonus";
}
