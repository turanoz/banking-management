namespace BankingManagement.Core.DTOs.Account;

public class AccountDto
{
    public Guid AccountId { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
}