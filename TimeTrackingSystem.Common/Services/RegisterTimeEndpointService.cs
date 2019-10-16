using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Misc;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public class RegisterTimeEndpointService : ServiceBase<RegisterTimeEndpoint, RegisterTimeEndpointViewModel>, IRegisterTimeEndpointService
    {
        private IRepository<RegisterTimeEndpoint> _enpointRepository;
        private IEmployeeService _employeeService;
        public RegisterTimeEndpointService(IRepositoryWrapper repositoryWrapper, IMapper mapper, IEmployeeService employeeService) : base(repositoryWrapper, mapper)
        {
            _enpointRepository = repositoryWrapper.GetRepository<RegisterTimeEndpoint>();
            _employeeService = employeeService;
        }

        public int GenerateToken(int id)
        {
            var endpoint = GetEntityByID(id);
            endpoint.SecurityToken = EndpointTokenHelper.GenerateRandomToken();
            return Update(endpoint);
        }

        public int ValidateEndpoint(string name, string securityToken)
        {
            var query = _enpointRepository.FindAll()
                .Where(e => e.Name.Equals(name) && e.SecurityToken.Equals(securityToken));
            if (query.Any())
                return query.FirstOrDefault().ID;
            else
                return -1;
        }

        public RegisterTimeResponse RegisterTime(int endpointID, string identityCode)
        {
            var endpoint = GetEntityByID(endpointID);
            var employee = _employeeService.GetEmployeeByIdentityCode(identityCode);
            var check = false;
            if (endpoint != null && employee != null)
            {
                if (endpoint.EndpointType.Equals(EndpointType.Entrance.ToString()))
                {
                    if (!_employeeService.IsEmployeeInWork(employee.ID))
                    {
                        check = _employeeService.RegisterEventIn(employee.ID, endpoint.ID);
                    }
                    else
                    {
                        return new RegisterTimeResponse(){ResponseType = RegisterTimeResponseType.InWork};
                    }

                }
                else if (endpoint.EndpointType.Equals(EndpointType.Exit.ToString()))
                {
                    if (_employeeService.IsEmployeeInWork(employee.ID))
                    {
                        check =  _employeeService.RegisterEventOut(employee.ID, endpoint.ID);
                    }
                    else
                    {
                        return new RegisterTimeResponse() { ResponseType = RegisterTimeResponseType.OutWork };
                    }
                }

                if (check)
                {
                    //TODO czas
                    return new RegisterTimeResponse() { ResponseType = RegisterTimeResponseType.Success, WorkTime = new TimeSpan()};
                }
            }

            return new RegisterTimeResponse() { ResponseType = RegisterTimeResponseType.Error };
        }
    }
}
