using System.Collections.Generic;
using ExpressMapperExampleTests.EntityFramework;

namespace ExpressMapperExampleTests.Repository
{
    public interface IAddressPersonRepository : IRepository<AddressDetail>
    {
        IEnumerable<AddressDetail> GetByPersonId(int id);
    }
}
