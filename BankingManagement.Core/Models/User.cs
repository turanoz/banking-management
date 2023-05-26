namespace BankingManagement.Core.Models;

public class User
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; } // Salt used for the password hash
    public DateTime CreatedDate { get; set; } // The date the user account was created
    public DateTime LastLoginDate { get; set; } // The date of the user's last login
    public Role Role { get; set; } // Navigation property
    public ICollection<Account> Accounts { get; set; } // Collection of the user's bank accounts
    public ICollection<AuditLog> AuditLogs { get; set; } // Collection of the user's audit logs
}
