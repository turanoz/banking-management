namespace BankingManagement.Core.DTOs.AuditLog;

public class AuditLogDto
{
    public Guid AuditLogId { get; set; }
    public string Action { get; set; }
    public string IPAddress { get; set; }
    public DateTime ActionTime { get; set; }
    public Guid UserId { get; set; }
}