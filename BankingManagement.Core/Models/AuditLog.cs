namespace BankingManagement.Core.Models;

public class AuditLog
{
    public Guid AuditId { get; set; }
    public Guid UserId { get; set; }
    public string Action { get; set; } // e.g., "Login", "Logout", "Transfer"
    public DateTime ActionTime { get; set; }
    public string IPAddress { get; set; } // The IP address from which the action was taken
    public User User { get; set; } // Navigation property
}
