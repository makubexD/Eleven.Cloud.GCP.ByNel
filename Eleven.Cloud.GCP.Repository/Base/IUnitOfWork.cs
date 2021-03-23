using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Repository.Base
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        IExecutionStrategy CreateExecutionStrategy();
        IDbContextTransaction GetCurrentTransaction();
        IDbContextTransaction BeginTransaction();
        Task CommitAsync();
    }
}
