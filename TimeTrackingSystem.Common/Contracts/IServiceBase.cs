using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingSystem.Common.DTO;

namespace TimeTrackingSystem.Common.Services
{
    public interface IServiceBase<VM>
    {
        List<VM> FindAll();

        FindByConditionResponse<VM> FindByConditions(int pageIndex, int pageSize, string searchExpression,
            string sortColumn, string sortOrder);
        VM GetByID(int id);
        int Insert(VM item);
        int Update(VM item);
        int Delete(int id);
    }
}
