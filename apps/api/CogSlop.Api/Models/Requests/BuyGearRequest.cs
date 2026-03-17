using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class BuyGearRequest
{
    [Range(1, 20)]
    public int Quantity { get; set; } = 1;
}
