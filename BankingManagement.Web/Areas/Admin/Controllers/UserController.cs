using AutoMapper;
using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.GetUsersInRoleAsync("Customer");
        var usersDto = _mapper.Map<IList<UserDto>>(users);
        if (users.Count == 0)
        {
            return View(CustomResponseDto<IList<UserDto>>.Info("No user found"));
        }

        return View(CustomResponseDto<IList<UserDto>>.Success(usersDto));
    }

    public async Task<IActionResult> Detail(Guid id)
    {
        var user = await _userManager.Users
            .Include(x => x.Accounts)
            .ThenInclude(x=>x.Transactions.OrderByDescending(x=>x.TransactionTime))
            .Include(x => x.AuditLogs.OrderByDescending(x=>x.ActionTime))
            .Where(x => x.Id == id)
            .SingleAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return View(userDto);
    }
}