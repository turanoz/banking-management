namespace BankingManagement.Core.DTOs.Account;

public class AccountCreateDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Balance { get; set; }
}