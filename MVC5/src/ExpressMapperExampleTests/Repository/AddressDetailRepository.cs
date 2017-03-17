using System.Collections.Generic;
using System.Linq;
using ExpressMapperExampleTests.EntityFramework;

namespace ExpressMapperExampleTests.Repository
{
    public class AddressDetailRepository : IAddressPersonRepository
    {
        private ExpressTestMapperEntities _context;

        public AddressDetailRepository(ExpressTestMapperEntities context)
        {
            _context = context;
        }

        public IEnumerable<AddressDetail> GetList()
        {
            return _context.AddressDetails.ToList();
        }

        public AddressDetail GetById(int id)
        {
            return _context.AddressDetails.Find(id);
        }

        public IEnumerable<AddressDetail> GetByPersonId(int id)
        {
            return _context.AddressDetails.Where(x => x.PersonId == id).ToList();
        }
    }
}
