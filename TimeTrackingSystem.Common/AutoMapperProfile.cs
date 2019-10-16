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
            CreateMap<RegisterTimeEndpoint, RegisterTimeEndpointViewModel>()
                .ForMember(e => e.EndpointType,
                    op => op.ConvertUsing(new StringToEndpointTypeConverter()));
            CreateMap<RegisterTimeEndpointViewModel, RegisterTimeEndpoint>().ForMember(e => e.EndpointType,
                    opt => opt.ConvertUsing(new EndpointTypeToStringConverter()))
                .ForMember(e => e.SecurityToken, op => op.UseDestinationValue());
        }

        //TODO hanldowanie jakos blednych enumow
        private class StringToEndpointTypeConverter: IValueConverter<string, EndpointType>
        {
            public EndpointType Convert(string sourceMember, ResolutionContext context)
            {
                if (Enum.TryParse(typeof(EndpointType), sourceMember, out var parsedResult))
                    return (EndpointType) parsedResult;
                throw new ArgumentException();
            }
        }

        private class EndpointTypeToStringConverter : IValueConverter<EndpointType, string>
        { 
            public string Convert(EndpointType sourceMember, ResolutionContext context)
            {
                if (Enum.IsDefined(typeof(EndpointType), sourceMember))
                    return sourceMember.ToString();
                throw new ArgumentException();
            }
        }
    }
}
