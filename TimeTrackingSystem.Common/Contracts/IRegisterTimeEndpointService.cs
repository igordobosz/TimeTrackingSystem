using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Services;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Common.Contracts
{
    public interface IRegisterTimeEndpointService : IServiceBase<RegisterTimeEndpointViewModel>
    {
        int GenerateToken(int id);
        int ValidateEndpoint(string name, string securityToken);
        RegisterTimeResponse RegisterTime(int endpointID, string identityCode);
    }
}
