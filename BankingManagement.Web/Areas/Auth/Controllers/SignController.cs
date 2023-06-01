using System.Security.Claims;
using BankingManagement.Core.Constants;
using BankingManagement.Core.DTOs.Sign;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.Web.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class SignController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAuditLogService _auditLogService;


        public SignController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, IAuditLogService auditLogService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (roles.Contains("Customer"))
                {
                    await _auditLogService.CreateAuditLogAsync(user.Id, AuditLogConstant.Login);
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
            }

            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    loginDto.Email,
                    loginDto.Password,
                    loginDto.RememberMe,
                    lockoutOnFailure: false
                );

                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (roles.Contains("Customer"))
                    {
                        await _auditLogService.CreateAuditLogAsync(user.Id, AuditLogConstant.Login);
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                }

                if (user != null)
                {
                    await _auditLogService.CreateAuditLogAsync(user.Id, AuditLogConstant.LoginFailed);
                }

                ModelState.AddModelError(string.Empty, "Email or password is invalid.");
            }

            return View(loginDto);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerDto.Email, Email = registerDto.Email, FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName
                };
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var roles = new[] { "Customer", "Admin" };

                    foreach (var role in roles)
                    {
                        if (!await _roleManager.RoleExistsAsync(role))
                        {
                            await _roleManager.CreateAsync(new Role() { Name = role });
                        }
                    }

                    await _userManager.AddToRoleAsync(user, "Customer");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerDto);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _auditLogService.CreateAuditLogAsync(userId, AuditLogConstant.Logout);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Sign");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}