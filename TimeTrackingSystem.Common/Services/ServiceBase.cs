using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Data;

namespace TimeTrackingSystem.Common.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected IRepositoryWrapper RepositoryWrapper;
        public ServiceBase(IRepositoryWrapper repositoryWrapper)
        {
            this.RepositoryWrapper = repositoryWrapper;
        }
    }
}
