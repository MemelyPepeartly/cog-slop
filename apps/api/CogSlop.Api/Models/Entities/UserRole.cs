namespace CogSlop.Api.Models.Entities;

public class UserRole
{
    public int UserAccountId { get; set; }

    public int RoleId { get; set; }

    public UserAccount UserAccount { get; set; } = null!;

    public Role Role { get; set; } = null!;
}
