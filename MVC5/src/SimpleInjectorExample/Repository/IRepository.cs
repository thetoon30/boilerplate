using System.Collections.Generic;

namespace SimpleInjectorExample.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetById(int id);
        IEnumerable<T> Add(T entity);
        IEnumerable<T> Update(T entity );
        IEnumerable<T> Delete(int id);
    }
}