using System.Security.Claims;
using BankingManagement.Core.Constants;
using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class ProfileController : Controller
{
    // GET
    private readonly UserManager<User> _userManager;
    private readonly IAuditLogService _auditLogService;

    public ProfileController(UserManager<User> userManager, IAuditLogService auditLogService)
    {
        _userManager = userManager;
        _auditLogService = auditLogService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address
        };
        
        await _auditLogService.CreateAuditLogAsync(user.Id, AuditLogConstant.ProfileView);
        return View(userDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(UserDto userDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Address = userDto.Address;



        await _userManager.UpdateAsync(user);
        
        await _auditLogService.CreateAuditLogAsync(user.Id, AuditLogConstant.ProfileUpdate);

        
        return View(nameof(Index));
    }
}