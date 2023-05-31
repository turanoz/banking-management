using Microsoft.AspNetCore.Identity;

namespace BankingManagement.Core.Models;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Address { get; set; }

    public ICollection<Account> Accounts { get; set; }
    public ICollection<AuditLog> AuditLogs { get; set; }
}