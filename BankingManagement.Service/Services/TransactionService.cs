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
        var transactions = await _unitOfWork.TransactionRepository.GetAll().Include(x=>x.Account).Include(x=>x.ReceiverAccount).ToListAsync();
        return CustomResponseDto<IEnumerable<TransactionDto>>.Success(
            _mapper.Map<IEnumerable<TransactionDto>>(transactions),
            "Transactions found.");
    }

    public async Task<CustomResponseDto<IEnumerable<TransactionDto>>> GetAllTransactionsByAccountIdAsync(Guid accountId)
    {
        var transactions = await _unitOfWork.TransactionRepository.GetAll().Where(x => x.AccountId == accountId).Include(x=>x.Account).Include(x=>x.ReceiverAccount)
            .ToListAsync();
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

    public async Task<CustomResponseDto<TransactionDto>> CreateTransferTransactionAsync(
        TransactionTransferDto transactionTransferDto)
    {
        // Get the sender account
        var senderAccount = await _unitOfWork.AccountRepository.GetByIdAsync(transactionTransferDto.AccountId);
        if (senderAccount == null)
        {
            return CustomResponseDto<TransactionDto>.Error("Sender account not found.");
        }

        // Check if sender has enough balance
        if (senderAccount.Balance < transactionTransferDto.Amount)
        {
            return CustomResponseDto<TransactionDto>.Error("Sender account does not have enough balance.");
        }

        // Get the receiver account
        var receiverAccount =
            await _unitOfWork.AccountRepository.GetByIdAsync(transactionTransferDto.ReceiverAccountId);
        if (receiverAccount == null)
        {
            return CustomResponseDto<TransactionDto>.Error("Receiver account not found.");
        }

        // Create the sender transaction
        var senderTransaction = new Transaction
        {
            TransactionType = "Transfer",
            Amount = transactionTransferDto.Amount,
            TransactionTime = DateTime.Now,
            AccountId = senderAccount.Id,
            ReceiverAccountId = receiverAccount.Id
        };
        await _unitOfWork.TransactionRepository.CreateAsync(senderTransaction);

        // Update sender account balance
        senderAccount.Balance -= transactionTransferDto.Amount;

        // Create the receiver transaction
        var receiverTransaction = new Transaction
        {
            TransactionType = "Deposit",
            Amount = transactionTransferDto.Amount,
            TransactionTime = DateTime.Now,
            AccountId = receiverAccount.Id,
            ReceiverAccountId = senderAccount.Id
        };
        await _unitOfWork.TransactionRepository.CreateAsync(receiverTransaction);

        // Update receiver account balance
        receiverAccount.Balance += transactionTransferDto.Amount;

        // Commit changes to database
        await _unitOfWork.CommitAsync();

        // Map the transaction entity to a DTO and return it
        return CustomResponseDto<TransactionDto>.Success(_mapper.Map<TransactionDto>(senderTransaction),
            "Transfer transaction created.");
    }
}