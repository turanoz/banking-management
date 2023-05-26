namespace BankingManagement.Core.Models;

public class Role
{
    public Guid RoleId { get; set; }
    public string Name { get; set; } // e.g., "Admin", "Customer"
    public ICollection<User> Users { get; set; } // Collection of users with this role
}