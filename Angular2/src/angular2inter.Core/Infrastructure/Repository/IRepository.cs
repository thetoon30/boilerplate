using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetById(int id);
        void Add(T entity);
        IEnumerable<T> Update(T entity);
        void Delete(T entity);

        IQueryable<T> Table { get; }
    }
}
