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
        var auditLogs = await _auditLogService.GetAllAuditLogsAsync();

        return View(auditLogs);
    }
}