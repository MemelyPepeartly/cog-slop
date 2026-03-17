namespace CogSlop.Api.Models.Entities;

public class CogSession
{
    public int CogSessionId { get; set; }

    public int UserAccountId { get; set; }

    public DateTime CogInAtUtc { get; set; }

    public DateTime? CogOutAtUtc { get; set; }

    public string? CogInNote { get; set; }

    public string? CogOutNote { get; set; }

    public UserAccount UserAccount { get; set; } = null!;
}
