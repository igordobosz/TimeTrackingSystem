using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public class EmployeeService : ServiceBase<Employee, EmployeeViewModel>, IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;
        public EmployeeService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper)
        {
            _employeeRepository = repositoryWrapper.GetRepository<Employee>();
        }
    }
}
