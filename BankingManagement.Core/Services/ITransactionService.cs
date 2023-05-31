using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.Transaction;
using BankingManagement.Core.Models;

namespace BankingManagement.Core.Services;

public interface ITransactionService
{
    Task<CustomResponseDto<IEnumerable<TransactionDto>>> GetAllTransactionsAsync();
    Task<CustomResponseDto<IEnumerable<TransactionDto>>> GetAllTransactionsByAccountIdAsync(Guid accountId);
    Task<CustomResponseDto<TransactionDto>> GetTransactionByIdAsync(Guid id);
    Task<CustomResponseDto<TransactionDto>> CreateTransactionAsync(TransactionCreateDto newTransaction);
    Task<CustomResponseDto<TransactionDto>> CreateTransferTransactionAsync(TransactionTransferDto transactionTransferDto);

    // Transactions usually don't have update/delete operations, but you can add if necessary
}
