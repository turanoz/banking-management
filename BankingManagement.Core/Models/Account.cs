namespace BankingManagement.Core.Models;

public class Account: BaseEntity
{
    
    public string Name { get; set; } 
    public string Type { get; set; } // e.g., "Savings", "Checking"
    public decimal Balance { get; set; }
    public DateTime OpenedDate { get; set; } // The date the account was opened
    public Guid UserId { get; set; }
    public User User { get; set; } // Navigation property
    public ICollection<Transaction> Transactions { get; set; } // Collection of the account's transactions
}