using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Customer.Controllers;
[Area("Customer")]
public class ProfileController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}