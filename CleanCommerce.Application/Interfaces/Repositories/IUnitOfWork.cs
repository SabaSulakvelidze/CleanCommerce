namespace CleanCommerce.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransationAsync();
        Task CommitTransationAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
