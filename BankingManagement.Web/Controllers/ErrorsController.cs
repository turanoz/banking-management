using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Controllers;

public class ErrorsController : Controller
{
    [Route("Errors/404")]
    public IActionResult Error404()
    {
        return View("~/Views/Shared/Errors/Error404.cshtml");
    }
}