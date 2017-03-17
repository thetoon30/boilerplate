using angular2inter.Core.Helper;
using angular2inter.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace angular2inter.Core.Infrastructure.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser FindByName(string name);
        Task<ApplicationUser> FindByNameAsync(string name);
        Task<ApplicationUser> FindByEmailAsync(string email);
        PagedResult<ApplicationUser> FindAll(int start, int max);
    }
}