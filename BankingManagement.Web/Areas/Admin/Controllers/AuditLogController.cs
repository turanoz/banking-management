using System.Security.Claims;
using BankingManagement.Core.Constants;
using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AuditLogController : Controller
{
    private readonly IAuditLogService _auditLogService;

    public AuditLogController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _auditLogService.CreateAuditLogAsync(Guid.Parse(userId), AuditLogConstant.ViewAuditLog);
        var auditLogs = await _auditLogService.GetAllAuditLogsAsync();

        return View(auditLogs);
    }
}