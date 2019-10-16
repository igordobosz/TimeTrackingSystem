using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Controllers
{
    [Route("api/RegisterTimeEndpoint")]
    public class RegisterTimeEndpointController : CrudController<IRegisterTimeEndpointService, RegisterTimeEndpointViewModel>
    {
        public RegisterTimeEndpointController(IRegisterTimeEndpointService baseService) : base(baseService)
        {
        }

        [HttpPost]
        [Route("GenerateToken")]
        public CrudResponse GenerateToken(int id)
        {
            return ConvertServiceResponseToCrudResponse(_baseService.GenerateToken(id));
        }

        [HttpGet]
        [Route("ValidateEndpoint")]
        public CrudResponse ValidateEndpoint(string name, string securityToken)
        {
            return ConvertServiceResponseToCrudResponse(_baseService.ValidateEndpoint(name, securityToken));
        }
        [HttpPost]
        [Route("RegisterTime")]
        public RegisterTimeResponse RegisterTime(int id, string identityCode)
        {
            return _baseService.RegisterTime(id, identityCode);
        }

    }
}
