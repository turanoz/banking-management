using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Customer.Controllers;
[Area("Customer")]
[Authorize(Roles = "Customer")]

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}