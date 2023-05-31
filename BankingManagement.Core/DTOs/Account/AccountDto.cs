namespace BankingManagement.Core.DTOs.Account;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Type { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpenedDate { get; set; }
}