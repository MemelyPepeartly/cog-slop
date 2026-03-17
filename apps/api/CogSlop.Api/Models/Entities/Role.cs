namespace CogSlop.Api.Models.Entities;

public class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
