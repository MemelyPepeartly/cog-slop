namespace CogSlop.Api.Models.Entities;

public class CogTransaction
{
    public int CogTransactionId { get; set; }

    public int UserAccountId { get; set; }

    public int Amount { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? GearItemId { get; set; }

    public int? GrantedByUserAccountId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public UserAccount UserAccount { get; set; } = null!;

    public GearItem? GearItem { get; set; }

    public UserAccount? GrantedByUserAccount { get; set; }
}
