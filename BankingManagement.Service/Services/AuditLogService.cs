using AutoMapper;
using BankingManagement.Core.DTOs.AuditLog;
using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditLogService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CustomResponseDto<IEnumerable<AuditLogDto>>> GetAllAuditLogsAsync()
    {
        var auditLogs = await _unitOfWork.AuditLogRepository.GetAll().OrderByDescending(x=>x.ActionTime).ToListAsync();
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

    public async Task<CustomResponseDto<AuditLogDto>> CreateAuditLogAsync(Guid userId, string action)
    {
        var auditLog = new AuditLog
        {
            UserId = userId,
            Action = action,
            IPAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
            ActionTime = DateTime.Now
        };

        await _unitOfWork.AuditLogRepository.CreateAsync(auditLog);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<AuditLogDto>.Success(_mapper.Map<AuditLogDto>(auditLog), "AuditLog created.");
    }
}