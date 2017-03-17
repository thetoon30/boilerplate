using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
