using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingSystem.Common.Contracts;

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

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_appDbContext);
            }
            return (IRepository<TEntity>)_repositories[type];
        }

        public bool SaveChanges()
        {
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
