using AutoMapper;
using BankingManagement.Core.DTOs.Account;
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

    public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
    {
        var accounts = await _unitOfWork.AccountRepository.GetAll().ToListAsync();
        return _mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task<AccountDto> GetAccountByIdAsync(Guid id)
    {
        var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        return _mapper.Map<AccountDto>(account);
    }

    public async Task<AccountDto> CreateAccountAsync(AccountCreateDto newAccount)
    {
        var accountEntity = _mapper.Map<Account>(newAccount);
        await _unitOfWork.AccountRepository.CreateAsync(accountEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<AccountDto>(accountEntity);
    }

    public async Task<AccountDto> UpdateAccountAsync(Guid id, AccountUpdateDto updatedAccount)
    {
        var accountEntity = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        _mapper.Map(updatedAccount, accountEntity);
        _unitOfWork.AccountRepository.Update(accountEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<AccountDto>(accountEntity);
    }

    public async Task<bool> DeleteAccountAsync(Guid id)
    {
        var accountEntity = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        if (accountEntity != null)
        {
            _unitOfWork.AccountRepository.Delete(accountEntity);
            await _unitOfWork.CommitAsync();
            return true;
        }
        return false;
    }
}
