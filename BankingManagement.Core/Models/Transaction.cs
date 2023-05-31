namespace BankingManagement.Core.Models;

public class Transaction:BaseEntity
{
    
    public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal", "Transfer"
    public decimal Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public Guid AccountId { get; set; }
    public Guid ReceiverAccountId  { get; set; }
    public Account Account { get; set; } // Navigation property
    public Account ReceiverAccount { get; set; } // Navigation property
}