using System.Security.Claims;
using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProfileController : Controller
{
    // GET
    private readonly UserManager<User> _userManager;

    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
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
        
        
        return View(nameof(Index));
    }
}