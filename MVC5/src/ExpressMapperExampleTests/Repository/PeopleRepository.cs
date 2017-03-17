using System.Collections.Generic;
using System.Linq;
using ExpressMapperExampleTests.EntityFramework;

namespace ExpressMapperExampleTests.Repository
{
    public class PeopleRepository : IRepository<Person>
    {
        private ExpressTestMapperEntities _context;

        public PeopleRepository(ExpressTestMapperEntities context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetList()
        {
            return _context.People.ToList();
        }

        public Person GetById(int id)
        {
            return _context.People.Find(id);
        }
    }
}
