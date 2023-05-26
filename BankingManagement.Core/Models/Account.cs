namespace BankingManagement.Core.Models;

public class Account
{
    public Guid AccountId { get; set; }
    public Guid UserId { get; set; }
    public string AccountNumber { get; set; } // Unique identifier for the account
    public string AccountType { get; set; } // e.g., "Savings", "Checking"
    public decimal Balance { get; set; }
    public DateTime OpenedDate { get; set; } // The date the account was opened
    public User User { get; set; } // Navigation property
    public ICollection<Transaction> Transactions { get; set; } // Collection of the account's transactions
}