namespace BankingManagement.Core.DTOs.User;

public class UserUpdateDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Avatar { get; set; }
}