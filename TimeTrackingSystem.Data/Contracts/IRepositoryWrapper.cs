using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Contracts
{
    public interface IRepositoryWrapper
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<bool> SaveChanges();
    }
}
