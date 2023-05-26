using BankingManagement.Core.DTOs.Account;

namespace BankingManagement.Core.Services;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
    Task<AccountDto> GetAccountByIdAsync(Guid id);
    Task<AccountDto> CreateAccountAsync(AccountCreateDto newAccount);
    Task<AccountDto> UpdateAccountAsync(Guid id, AccountUpdateDto updatedAccount);
    Task<bool> DeleteAccountAsync(Guid id);
    // Add other necessary methods
}