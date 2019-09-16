using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Data;

namespace TimeTrackingSystem.Common.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected IRepositoryWrapper _repositoryWrapper;
        public ServiceBase(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        public Task<List<T>> FindAll()
        {
            return _repositoryWrapper.GetRepository<T>().FindAll().ToListAsync();
        }

        public Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _repositoryWrapper.GetRepository<T>().FindByCondition(expression).ToListAsync();
        }

        public Task<bool> Insert(T item)
        {
            _repositoryWrapper.GetRepository<T>().Insert(item);
            return _repositoryWrapper.SaveChanges();
        }

        public Task<bool> Update(T item)
        {
            _repositoryWrapper.GetRepository<T>().Update(item);
            return _repositoryWrapper.SaveChanges();
        }

        public Task<bool> Delete(int id)
        {
            _repositoryWrapper.GetRepository<T>().Delete(id);
            return _repositoryWrapper.SaveChanges();
        }
    }
}
