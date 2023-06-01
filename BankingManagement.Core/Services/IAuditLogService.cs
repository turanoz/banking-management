using BankingManagement.Core.DTOs.AuditLog;
using BankingManagement.Core.DTOs.Response;

namespace BankingManagement.Core.Services;

public interface IAuditLogService
{
    Task<CustomResponseDto<IEnumerable<AuditLogDto>>> GetAllAuditLogsAsync();
    Task<CustomResponseDto<AuditLogDto>> GetAuditLogByIdAsync(Guid id);
    Task<CustomResponseDto<AuditLogDto>> CreateAuditLogAsync(Guid userId,string action);
}