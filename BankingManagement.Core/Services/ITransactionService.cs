using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.Transaction;

namespace BankingManagement.Core.Services;

public interface ITransactionService
{
    Task<CustomResponseDto<IEnumerable<TransactionDto>>> GetAllTransactionsAsync();
    Task<CustomResponseDto<TransactionDto>> GetTransactionByIdAsync(Guid id);
    Task<CustomResponseDto<TransactionDto>> CreateTransactionAsync(TransactionCreateDto newTransaction);
    // Transactions usually don't have update/delete operations, but you can add if necessary
}
