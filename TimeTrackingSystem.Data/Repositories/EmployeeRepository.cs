using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Data.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public Task<IEnumerable<Employee>> Get()
        {
        }

        public Task<Employee> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
