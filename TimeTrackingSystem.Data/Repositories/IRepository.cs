using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingSystem.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task Insert(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(int id);
    }
}
