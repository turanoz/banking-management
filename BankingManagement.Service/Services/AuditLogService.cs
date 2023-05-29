using AutoMapper;
using BankingManagement.Core.DTOs.AuditLog;
using BankingManagement.Core.DTOs.Response;
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

    public async Task<CustomResponseDto<IEnumerable<AuditLogDto>>> GetAllAuditLogsAsync()
    {
        var auditLogs = await _unitOfWork.AuditLogRepository.GetAll().ToListAsync();
        return CustomResponseDto<IEnumerable<AuditLogDto>>.Success(_mapper.Map<IEnumerable<AuditLogDto>>(auditLogs),
            "AuditLogs found.");
    }

    public async Task<CustomResponseDto<AuditLogDto>> GetAuditLogByIdAsync(Guid id)
    {
        var auditLog = await _unitOfWork.AuditLogRepository.GetByIdAsync(id);
        return auditLog is null
            ? CustomResponseDto<AuditLogDto>.Error("AuditLog not found.")
            : CustomResponseDto<AuditLogDto>.Success(_mapper.Map<AuditLogDto>(auditLog), "AuditLog found.");
    }

    public async Task<CustomResponseDto<AuditLogDto>> CreateAuditLogAsync(AuditLogCreateDto newAuditLog)
    {
        var auditLogEntity = _mapper.Map<AuditLog>(newAuditLog);
        await _unitOfWork.AuditLogRepository.CreateAsync(auditLogEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<AuditLogDto>.Success(_mapper.Map<AuditLogDto>(auditLogEntity), "AuditLog created.");
    }
}