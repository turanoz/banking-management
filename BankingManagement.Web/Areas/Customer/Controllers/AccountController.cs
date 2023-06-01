using System.Security.Claims;
using BankingManagement.Core.Constants;
using BankingManagement.Core.DTOs.Account;
using BankingManagement.Core.DTOs.Transaction;
using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ITransactionService _transactionService;
    private readonly IAuditLogService _auditLogService;

    public AccountController(IAccountService accountService, ITransactionService transactionService, IAuditLogService auditLogService)
    {
        _accountService = accountService;
        _transactionService = transactionService;
        _auditLogService = auditLogService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _auditLogService.CreateAuditLogAsync(Guid.Parse(userId), AuditLogConstant.ViewAccountList);
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
        
        await _auditLogService.CreateAuditLogAsync(accountCreateDto.UserId, AuditLogConstant.CreateAccount);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Transaction(Guid accountId)
    {
        var transactions = await _transactionService.GetAllTransactionsByAccountIdAsync(accountId);
        var account = await _accountService.GetAccountByIdAsync(accountId);
        ViewData["accountName"] = account.Data.Name;
        ViewData["accountId"] = accountId;
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _auditLogService.CreateAuditLogAsync(Guid.Parse(userId), AuditLogConstant.ViewTransactionList);
        return View(transactions);
    }

    public IActionResult TransactionCreate(Guid accountId)
    {
        var transactionTransferDto = new TransactionTransferDto
        {
            AccountId = accountId
        };
        return View(transactionTransferDto);
    }

    [HttpPost]
    public async Task<IActionResult> TransactionCreate(TransactionTransferDto transactionTransferDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _transactionService.CreateTransferTransactionAsync(transactionTransferDto);

        await _auditLogService.CreateAuditLogAsync(Guid.Parse(userId), AuditLogConstant.CreateTransaction);
        
        ViewData["Errors"] = response.Errors;
        return RedirectToAction(nameof(Transaction), new {accountId = transactionTransferDto.AccountId});
    }
}