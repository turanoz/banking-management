using AutoMapper;
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

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        var transactions = await _unitOfWork.TransactionRepository.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }

    public async Task<TransactionDto> GetTransactionByIdAsync(Guid id)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<TransactionDto> CreateTransactionAsync(TransactionCreateDto newTransaction)
    {
        var transactionEntity = _mapper.Map<Transaction>(newTransaction);
        await _unitOfWork.TransactionRepository.CreateAsync(transactionEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<TransactionDto>(transactionEntity);
    }
}
