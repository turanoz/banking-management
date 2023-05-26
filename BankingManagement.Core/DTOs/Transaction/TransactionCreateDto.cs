namespace BankingManagement.Core.DTOs.Transaction;

public class TransactionCreateDto
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
}