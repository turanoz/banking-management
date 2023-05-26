using AutoMapper;
using BankingManagement.Core.DTOs.AuditLog;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuditLogService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync()
    {
        var auditLogs = await _unitOfWork.AuditLogRepository.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<AuditLogDto>>(auditLogs);
    }

    public async Task<AuditLogDto> GetAuditLogByIdAsync(Guid id)
    {
        var auditLog = await _unitOfWork.AuditLogRepository.GetByIdAsync(id);
        return _mapper.Map<AuditLogDto>(auditLog);
    }

    public async Task<AuditLogDto> CreateAuditLogAsync(AuditLogCreateDto newAuditLog)
    {
        var auditLogEntity = _mapper.Map<AuditLog>(newAuditLog);
        await _unitOfWork.AuditLogRepository.CreateAsync(auditLogEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<AuditLogDto>(auditLogEntity);
    }
}
