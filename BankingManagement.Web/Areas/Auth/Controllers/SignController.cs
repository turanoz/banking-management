using BankingManagement.Core.DTOs.Sign;
using BankingManagement.Core.Models;
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


        public SignController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
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

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(loginDto.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (roles.Contains("Customer"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                }

                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
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

                    await _userManager.AddToRoleAsync(user, "Admin"); // Varsayılan olarak "Customer" rolü atanıyor

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerDto);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Sign");
        }
    }
}