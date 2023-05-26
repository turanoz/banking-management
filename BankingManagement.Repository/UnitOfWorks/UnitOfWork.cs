using BankingManagement.Core.Models;
using BankingManagement.Core.Repositories;
using BankingManagement.Core.UnitOfWorks;

namespace BankingManagement.Repository.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<User> UserRepository { get; }
    public IRepository<Role> RoleRepository { get; }
    public IRepository<Account> AccountRepository { get; }
    public IRepository<Transaction> TransactionRepository { get; }
    public IRepository<AuditLog> AuditLogRepository { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        UserRepository = new Repositories.Repository<User>(context);
        RoleRepository = new Repositories.Repository<Role>(context);
        AccountRepository = new Repositories.Repository<Account>(context);
        TransactionRepository = new Repositories.Repository<Transaction>(context);
        AuditLogRepository = new Repositories.Repository<AuditLog>(context);
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
