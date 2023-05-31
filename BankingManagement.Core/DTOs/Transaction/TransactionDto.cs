namespace BankingManagement.Core.DTOs.Transaction;

public class TransactionDto
{
    public DateTime TransactionTime { get; set; }
    public string TransactionType { get; set; } // e.g., "Deposit", "Withdrawal", "Transfer"
    public Models.Account Account { get; set; } // Navigation property
    public Models.Account ReceiverAccount { get; set; } // Navigation property
    public decimal Amount { get; set; }
    
}