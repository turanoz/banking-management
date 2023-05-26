using BankingManagement.Core.Models;
using BankingManagement.Core.Repositories;

namespace BankingManagement.Core.UnitOfWorks;
public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<Account> AccountRepository { get; }
    IRepository<Transaction> TransactionRepository { get; }
    IRepository<AuditLog> AuditLogRepository { get; }
    Task<int> CommitAsync();
}
