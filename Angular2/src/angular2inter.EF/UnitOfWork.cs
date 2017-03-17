using angular2inter.Core;
using angular2inter.Core.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string msg = string.Empty;

                foreach (DbEntityValidationResult dbevr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbve in dbevr.ValidationErrors)
                    {
                        msg = msg + string.Format("Property: {0} - Error: {1}", dbve.PropertyName, dbve.ErrorMessage) + Environment.NewLine;
                    }
                }

                throw new Exception(msg, ex);
            }
        }

        public Task<int> CommitAsync()
        {
            try
            {
                return _dbContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                string msg = string.Empty;

                foreach (DbEntityValidationResult dbevr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbve in dbevr.ValidationErrors)
                    {
                        msg = msg + string.Format("Property: {0} - Error: {1}", dbve.PropertyName, dbve.ErrorMessage) + Environment.NewLine;
                    }
                }

                throw new Exception(msg, ex);
            }
        }

        public void Dispose()
        {
        }
    }
}
