﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;

namespace TimeTrackingSystem.Controllers
{
    [ApiController]
    public abstract class CrudController<T, VM> : ControllerBase where T : IServiceBase<VM> where VM : IViewModel
    {
        private readonly T _baseService;

        public CrudController(T baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        [Route("List")]
        public List<VM> List(int pageLenght, int pageIndex, int pageSize, int pagePreviousIndex, string searchExpression, string orderProperty, string orderType)
        {
            //TODO: SEARCHTERM IMPLEMENTATION AND ORDER
            return _baseService.FindAll();
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

        private CrudResponse ConvertServiceResponseToCrudResponse(int id)
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
