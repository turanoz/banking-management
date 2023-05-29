using AutoMapper;
using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.DTOs.Transaction;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<IEnumerable<TransactionDto>>> GetAllTransactionsAsync()
    {
        var transactions = await _unitOfWork.TransactionRepository.GetAll().ToListAsync();
        return CustomResponseDto<IEnumerable<TransactionDto>>.Success(
            _mapper.Map<IEnumerable<TransactionDto>>(transactions),
            "Transactions found.");
    }

    public async Task<CustomResponseDto<TransactionDto>> GetTransactionByIdAsync(Guid id)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
        return transaction is null
            ? CustomResponseDto<TransactionDto>.Error("Transaction not found.")
            : CustomResponseDto<TransactionDto>.Success(_mapper.Map<TransactionDto>(transaction), "Transaction found.");
    }

    public async Task<CustomResponseDto<TransactionDto>> CreateTransactionAsync(TransactionCreateDto newTransaction)
    {
        var transactionEntity = _mapper.Map<Transaction>(newTransaction);
        await _unitOfWork.TransactionRepository.CreateAsync(transactionEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<TransactionDto>.Success(_mapper.Map<TransactionDto>(transactionEntity),
            "Transaction created.");
    }
}