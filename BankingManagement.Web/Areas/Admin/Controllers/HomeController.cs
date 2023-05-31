using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    // GET

    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View();
    }
}