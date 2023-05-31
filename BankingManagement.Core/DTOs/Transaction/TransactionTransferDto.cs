namespace BankingManagement.Core.DTOs.Transaction;

public class TransactionTransferDto
{
    public Guid AccountId { get; set; }
    public Guid ReceiverAccountId { get; set; }
    public decimal Amount { get; set; }
}