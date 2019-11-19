using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;

namespace TimeTrackingSystem.Controllers
{
    [ApiController]
    public abstract class CrudController<T, VM> : ControllerBase where T : IServiceBase<VM> where VM : IViewModel
    {
        protected readonly T _baseService;

        public CrudController(T baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        [Route("List")]
        public virtual FindByConditionResponse<VM> List(int pageIndex, int pageSize, string searchExpression, string sortColumn, string sortOrder)
        {
            return _baseService.FindByConditions(pageIndex, pageSize, searchExpression, sortColumn, sortOrder);
        }

        [HttpGet]
        [Route("GetByID")]
        public ActionResult<VM> GetByID(int id)
        {
            var result = _baseService.GetByID(id);
            if (result == null)
                return NotFound();
            return result;
        }

        [HttpPost]
        [Route("Insert")]
        public CrudResponse Insert(VM item)
        {
            return ConvertServiceResponseToCrudResponse(_baseService.Insert(item));
        }

        [HttpPost]
        [Route("Update")]
        public CrudResponse Update(VM item)
        {
            return ConvertServiceResponseToCrudResponse(_baseService.Update(item));
        }

        [HttpPost]
        [Route("Delete")]
        public CrudResponse Delete(int id)
        {
            return ConvertServiceResponseToCrudResponse(_baseService.Delete(id));
        }

        protected CrudResponse ConvertServiceResponseToCrudResponse(int id)
        {
            CrudResponse res = new CrudResponse();
            if (id > 0)
            {
                res.ID = id;
                res.Success = true;
            }
            else
            {
                res.Success = false;
            }
            return res;
        }
    }
}
