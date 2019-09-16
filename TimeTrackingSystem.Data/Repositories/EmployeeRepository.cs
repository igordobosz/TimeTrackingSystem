using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
