using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingSystem.Common.Services
{
    public interface IServiceBase<T>
    {
        Task<List<T>> FindAll();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task<bool> Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(int id);
    }
}
