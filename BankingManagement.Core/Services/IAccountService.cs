using BankingManagement.Core.DTOs.Account;
using BankingManagement.Core.DTOs.Response;

namespace BankingManagement.Core.Services;

public interface IAccountService
{
    Task<CustomResponseDto<IEnumerable<AccountDto>>> GetAllAccountsAsync();
    Task<CustomResponseDto<AccountDto>> GetAccountByIdAsync(Guid id);
    Task<CustomResponseDto<AccountDto>> CreateAccountAsync(AccountCreateDto newAccount);
    Task<CustomResponseDto<AccountDto>> UpdateAccountAsync(Guid id, AccountUpdateDto updatedAccount);
    Task<CustomResponseDto<bool>> DeleteAccountAsync(Guid id);
}