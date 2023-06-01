using BankingManagement.Core.DTOs.Account;
using BankingManagement.Core.DTOs.AuditLog;

namespace BankingManagement.Core.DTOs.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public ICollection<AccountDto> Accounts { get; set; }
    public ICollection<AuditLogDto> AuditLogs { get; set; }
}