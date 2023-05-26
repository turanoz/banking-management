using BankingManagement.Core.DTOs.AuditLog;

namespace BankingManagement.Core.Services;

public interface IAuditLogService
{
    Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync();
    Task<AuditLogDto> GetAuditLogByIdAsync(Guid id);
    Task<AuditLogDto> CreateAuditLogAsync(AuditLogCreateDto newAuditLog);
    // Usually, audit logs don't have update/delete operations, but you can add if necessary
}
