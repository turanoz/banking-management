namespace BankingManagement.Core.DTOs.Transaction;

public class TransactionDto
{
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
}