using System.ComponentModel.DataAnnotations;

namespace CogSlop.Api.Models.Requests;

public class CogInRequest
{
    [MaxLength(300)]
    public string? Note { get; set; }
}
