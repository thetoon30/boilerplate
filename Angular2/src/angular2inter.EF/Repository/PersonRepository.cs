using angular2inter.Core.DataContext;
using angular2inter.Core.Infrastructure.Repository;
using angular2inter.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.EF.Repository
{
    public class PersonRepository : EFRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbContext dbContext)
            : base(dbContext) { }

        public override void Add(Person entity)
        {
            base.Add(entity);
        }

        public override void Delete(Person entity)
        {
            base.Delete(entity);
        }
    }
}
