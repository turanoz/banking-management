using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Controllers;

public class SignController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
}