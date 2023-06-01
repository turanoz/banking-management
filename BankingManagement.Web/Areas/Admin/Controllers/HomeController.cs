﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Admin.Controllers;

[Area("Admin")]

[Authorize(Roles = "Admin")]

public class HomeController : Controller
{
    // GET

   
    public IActionResult Index()
    {
        return View();
    }
}