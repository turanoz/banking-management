using System.ComponentModel.DataAnnotations;

namespace BankingManagement.Core.DTOs.Sign;

public class RegisterDto
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Password and confirm password must be the same")]
    public string ConfirmPassword { get; set; }
}