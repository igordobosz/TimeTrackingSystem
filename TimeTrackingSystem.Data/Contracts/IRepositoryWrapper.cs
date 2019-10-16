using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingSystem.Data.Contracts;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Contracts
{
    public interface IRepositoryWrapper
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
        bool SaveChanges();

        bool HasChanges();
    }
}
