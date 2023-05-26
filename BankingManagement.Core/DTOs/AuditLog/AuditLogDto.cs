namespace BankingManagement.Core.DTOs.AuditLog;

public class AuditLogDto
{
    public Guid AuditLogId { get; set; }
    public string Action { get; set; }
    public DateTime ActionDate { get; set; }
    public Guid UserId { get; set; }
}