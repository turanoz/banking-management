namespace BankingManagement.Core.DTOs.AuditLog;

public class AuditLogCreateDto
{
    public string Action { get; set; }
    public Guid UserId { get; set; }
}