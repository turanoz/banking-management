using BankingManagement.Core.DTOs.User;
using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();
        return View(users); // Pass the list of users to the view
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateDto newUser)
    {
        if (ModelState.IsValid)
        {
            var userDto = await _userService.CreateUserAsync(newUser);
            if (userDto != null)
                return RedirectToAction(nameof(Index));
            // Handle creation failure
        }
        return View(newUser);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null)
        {
            return NotFound();
        }
        var userUpdateDto = new UserUpdateDto
        {
            UserId = userDto.UserId,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            PhoneNumber = userDto.PhoneNumber,
            Address = userDto.Address,
            Avatar = userDto.Avatar
        };
        return View(userUpdateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UserUpdateDto updateUser)
    {
        if (id != updateUser.UserId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var success = await _userService.UpdateUserAsync(id,updateUser);
            if (success is not null)
                return RedirectToAction(nameof(Index));
            // Handle update failure
        }
        return View(updateUser);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (success)
            return RedirectToAction(nameof(Index));
        return NotFound();
    }
}