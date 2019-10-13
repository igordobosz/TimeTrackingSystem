using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<RegisterTimeEndpoint, RegisterTimeEndpointViewModel>();
            CreateMap<RegisterTimeEndpointViewModel, RegisterTimeEndpoint>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
