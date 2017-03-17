using InternalModule.Boilerplate.Core.DataContext;
using InternalModule.Boilerplate.Core.Helper;
using InternalModule.Boilerplate.Core.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.EF.Repository
{
    public abstract class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private readonly DbSet<T> _dbSet;

        protected EFRepository(IDbContext context)
        {
            _context = context;
            //_dbSet = context.Set<T>();
            var dbContext = context as DbContext;
            if (dbContext != null)
            {
                _dbSet = dbContext.Set<T>();
            }
        }

        protected internal IDbContext DbContext
        {
            get { return _context; }
        }

        protected internal DbSet<T> DbSet
        {
            get { return _dbSet; }
        }

        public IQueryable<T> Table
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetList()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Update(T entity)
        {
            throw new NotImplementedException();
        }

        protected static PagedResult<T> BuildPagedResult(IList<T> entities, int total)
        {
            return new PagedResult<T>(entities, total);
        }
    }
}
