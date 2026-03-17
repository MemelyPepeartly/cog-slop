namespace CogSlop.Api.Models.Entities;

public class UserAccount
{
    public int UserAccountId { get; set; }

    public string GoogleSubject { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime LastLoginAtUtc { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public ICollection<CogTransaction> CogTransactions { get; set; } = new List<CogTransaction>();

    public ICollection<CogTransaction> GrantedCogTransactions { get; set; } = new List<CogTransaction>();

    public ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();
}
