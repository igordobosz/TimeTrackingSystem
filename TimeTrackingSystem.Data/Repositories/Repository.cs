using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected AppDbContext _repositoryContext { get; set; }

        public Repository(AppDbContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this._repositoryContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._repositoryContext.Set<T>().Where(expression).AsQueryable();
        }

        public T GetByID(int id)
        {
            return this._repositoryContext.Set<T>().FirstOrDefault(e => e.ID == id);
        }

        public void Insert(T entity)
        {
            this._repositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this._repositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this._repositoryContext.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            this._repositoryContext.Set<T>().Remove(GetByID(id));
        }
    }
}
