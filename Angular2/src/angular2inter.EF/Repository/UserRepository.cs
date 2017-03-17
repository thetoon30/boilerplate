using angular2inter.Core.DataContext;
using angular2inter.Core.Helper;
using angular2inter.Core.Infrastructure.Repository;
using angular2inter.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.EF.Repository
{
    public class UserRepository : EFRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IDbContext dbContext)
            : base(dbContext) { }

        public override void Add(ApplicationUser entity)
        {
            base.Add(entity);
        }

        public override void Delete(ApplicationUser entity)
        {
            base.Delete(entity);
        }

        public ApplicationUser FindByName(string name)
        {
            return DbSet.SingleOrDefault(u => u.UserName == name);
        }

        public Task<ApplicationUser> FindByNameAsync(string name)
        {
            var user = DbSet.SingleOrDefault(u => u.UserName == name);
            return Task.FromResult<ApplicationUser>(user);
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return Task.FromResult<ApplicationUser>(DbSet.SingleOrDefault(u => u.Email == email));
        }

        public PagedResult<ApplicationUser> FindAll(int start, int max)
        {
            var user = DbSet.OrderBy(a => a.UserName).Skip(start).Take(max).ToList();

            var count = DbSet.Count();

            return BuildPagedResult(user, count);
        }
    }
}
