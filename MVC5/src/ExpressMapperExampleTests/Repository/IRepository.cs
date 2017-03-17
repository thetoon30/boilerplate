using System.Collections.Generic;

namespace ExpressMapperExampleTests.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetById(int id);
    }
}
