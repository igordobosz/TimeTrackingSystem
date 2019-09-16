using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Contracts
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository EmployeeRepository { get; }

        void Save();
    }
}
