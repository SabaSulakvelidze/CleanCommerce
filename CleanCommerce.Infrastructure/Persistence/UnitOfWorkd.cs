
using CleanCommerce.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging.Abstractions;

namespace CleanCommerce.Infrastructure.Persistence
{
    public class UnitOfWorkd(AppDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? transaction; 

        public async Task BeginTransationAsync()
        {
            transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransationAsync()
        {
            if (transaction is null)
                throw new InvalidOperationException("No Active transaction to commit.");

            await transaction.CommitAsync();

            await transaction.DisposeAsync();
            transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (transaction is null)
                return;

            await transaction.RollbackAsync();

            await transaction.DisposeAsync();
            transaction = null;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
