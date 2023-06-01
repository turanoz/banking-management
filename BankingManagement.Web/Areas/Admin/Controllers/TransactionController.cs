using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        return View(transactions);
    }
}