using AutoMapper;
using BankingManagement.Core.DTOs.Account;
using BankingManagement.Core.DTOs.Response;
using BankingManagement.Core.Models;
using BankingManagement.Core.Services;
using BankingManagement.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Service.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; // Use AutoMapper for mapping between entities and Dtos

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<IEnumerable<AccountDto>>> GetAllAccountsAsync()
    {
        var accounts = await _unitOfWork.AccountRepository.GetAll().ToListAsync();
        return CustomResponseDto<IEnumerable<AccountDto>>.Success(_mapper.Map<IEnumerable<AccountDto>>(accounts),
            "Accounts found.");
    }

    public async Task<CustomResponseDto<AccountDto>> GetAccountByIdAsync(Guid id)
    {
        var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        if (account is null)
        {
            return CustomResponseDto<AccountDto>.Error("Account not found.");
        }

        return CustomResponseDto<AccountDto>.Success(_mapper.Map<AccountDto>(account), "Account found.");
    }

    public async Task<CustomResponseDto<AccountDto>> CreateAccountAsync(AccountCreateDto newAccount)
    {
        var accountEntity = _mapper.Map<Account>(newAccount);
        await _unitOfWork.AccountRepository.CreateAsync(accountEntity);
        await _unitOfWork.CommitAsync();

        return CustomResponseDto<AccountDto>.Success(_mapper.Map<AccountDto>(accountEntity), "Account created.");
    }

    public async Task<CustomResponseDto<AccountDto>> UpdateAccountAsync(Guid id, AccountUpdateDto updatedAccount)
    {
        var accountEntity = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        if (accountEntity is null)
        {
            return CustomResponseDto<AccountDto>.Error("Account not found.");
        }

        _mapper.Map(updatedAccount, accountEntity);
        _unitOfWork.AccountRepository.Update(accountEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<AccountDto>.Success(_mapper.Map<AccountDto>(accountEntity), "Account updated.");
    }

    public async Task<CustomResponseDto<bool>> DeleteAccountAsync(Guid id)
    {
        var accountEntity = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        if (accountEntity is null)
        {
            return CustomResponseDto<bool>.Error("Account not found.");
        }

        _unitOfWork.AccountRepository.Delete(accountEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<bool>.Success(true, "Account deleted.");
    }
}