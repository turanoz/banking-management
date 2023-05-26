using BankingManagement.Core.DTOs.Transaction;

namespace BankingManagement.Core.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
    Task<TransactionDto> GetTransactionByIdAsync(Guid id);
    Task<TransactionDto> CreateTransactionAsync(TransactionCreateDto newTransaction);
    // Transactions usually don't have update/delete operations, but you can add if necessary
}
