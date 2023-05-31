using System.Security.Claims;
using BankingManagement.Core.DTOs.Account;
using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var accounts = await _accountService.GetAllAccountsByUserIdAsync(Guid.Parse(userId));
        return View(accounts);
    }

    public IActionResult Create()
    {
        return View(new AccountCreateDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountCreateDto accountCreateDto)
    {
        accountCreateDto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        await _accountService.CreateAccountAsync(accountCreateDto);

        return RedirectToAction(nameof(Index));
    }

    public Task<IActionResult> Transaction(Guid id)
    {
        throw new NotImplementedException();
    }
}