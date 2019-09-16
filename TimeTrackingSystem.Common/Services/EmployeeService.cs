using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public class EmployeeService : ServiceBase<Employee>, IEmployeeService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public EmployeeService(IRepositoryWrapper repositoryWrapper) : base(repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Employee> GetAll()
        {
            return _repositoryWrapper.EmployeeRepository.FindAll().ToList();
        }
    }
}
