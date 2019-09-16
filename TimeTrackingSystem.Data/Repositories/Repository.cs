using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingSystem.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _repositoryContext { get; set; }

        public Repository(AppDbContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this._repositoryContext.Set<T>().AsQueryable().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._repositoryContext.Set<T>().Where(expression).AsQueryable().AsNoTracking();
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
            var typeInfo = typeof(T).GetTypeInfo();
            var key = _repositoryContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<T>();
                property.SetValue(entity, id);
                _repositoryContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _repositoryContext.Set<T>().Find(id);
                if (entity != null) Delete(entity);
            }
        }
    }
}
