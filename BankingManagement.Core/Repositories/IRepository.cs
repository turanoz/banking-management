namespace BankingManagement.Core.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
