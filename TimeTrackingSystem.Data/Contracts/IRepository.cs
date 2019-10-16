using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingSystem.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T GetByID(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(int id);
    }
}
