using Eleven.Cloud.GCP.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext _mainDbContext;
        public DbContext Context => _mainDbContext as DbContext;

        public UnitOfWork(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Context.Database.CreateExecutionStrategy();
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return Context.Database.CurrentTransaction;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
