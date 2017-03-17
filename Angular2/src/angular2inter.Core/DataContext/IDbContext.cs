using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core.DataContext
{
    public interface IDbContext : IDisposable
    {
        //DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        //void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        //void SyncObjectsStatePostCommit();
    }
}
