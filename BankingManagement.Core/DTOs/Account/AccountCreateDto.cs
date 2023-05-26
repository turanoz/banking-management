namespace BankingManagement.Core.DTOs.Account;

public class AccountCreateDto
{
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
}