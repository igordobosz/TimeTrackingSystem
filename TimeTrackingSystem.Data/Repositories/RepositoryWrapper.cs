using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.Contracts;

namespace TimeTrackingSystem.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _appDbContext;
        public IEmployeeRepository _employeeRepository;

        public IEmployeeRepository EmployeeRepository
        {
            get { return _employeeRepository ?? (_employeeRepository = new EmployeeRepository(_appDbContext)); }
        }

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
