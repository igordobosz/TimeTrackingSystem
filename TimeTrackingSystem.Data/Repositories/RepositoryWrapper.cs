using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _appDbContext;
        private Dictionary<Type, object> _repositories;

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_appDbContext);
            }
            return (IRepository<TEntity>)_repositories[type];
        }

        //TODO handling unique
        public bool SaveChanges()
        {
            try
            {
                return _appDbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool HasChanges()
        {
            return _appDbContext.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                                         || e.State == EntityState.Modified
                                                         || e.State == EntityState.Deleted);
        }
    }
}
