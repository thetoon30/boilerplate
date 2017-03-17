using System.Collections.Generic;
using System.Linq;
using ExpressMapperExampleTests.EntityFramework;

namespace ExpressMapperExampleTests.Repository
{
    public class PersonDetailRepository : IRepository<PersonDetail>
    {
        private ExpressTestMapperEntities _context;

        public PersonDetailRepository(ExpressTestMapperEntities context)
        {
            _context = context;
        }

        public IEnumerable<PersonDetail> GetList()
        {
            return _context.PersonDetails.ToList();
        }

        public PersonDetail GetById(int id)
        {
            return _context.PersonDetails.Find(id);
        }
    }
}
