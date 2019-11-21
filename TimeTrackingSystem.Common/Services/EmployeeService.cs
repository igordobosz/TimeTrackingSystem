using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public class EmployeeService : ServiceBase<Employee, EmployeeViewModel>, IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;
        public EmployeeService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper)
        {
            _employeeRepository = repositoryWrapper.GetRepository<Employee>();
        }

        public Employee GetEmployeeByIdentityCode(string identityCode)
        {
            return _employeeRepository.FindByCondition(e => e.IdentityCode.Equals(identityCode)).FirstOrDefault();
        }

        protected override void ApplyFilters(ref IQueryable<Employee> query, EmployeeViewModel searchModel)
        {
            if (searchModel != null)
            {
                var entity = ViewModelToEntity(searchModel);
                //TODO ZROBIC WLASNE EXPRESSION NA TO
                query = query.Where(e =>
                    (e.Name ?? "").ToLower().Contains((entity.Name ?? "").ToLower()) && (e.Surename ?? "").ToLower().Contains((entity.Surename ?? "").ToLower()) && (entity.EmployeeGroupID.Equals(null) || e.EmployeeGroupID.Equals(entity.EmployeeGroupID)));
            }
        }

        public bool IsEmployeeInWork(int id)
        {
            var employee = GetEntityByID(id);
            var workRegi = _repositoryWrapper.GetRepository<WorkRegisterEvent>().FindAll();
            if (employee == null)
                return false;
            return employee.WorkRegisterEvents.Any(e => e.DateGoOut == DateTime.MinValue);
        }

        public WorkRegisterEvent FindLastWorkRegister(int id)
        {
            return GetEntityByID(id).WorkRegisterEvents.FirstOrDefault(e => e.DateGoOut == DateTime.MinValue);
        }

        public bool RegisterEventIn(int employeeId, int endpointId)
        {
            var employee = GetEntityByID(employeeId);
            var workRegisterEvent = new WorkRegisterEvent() { DateGoIn = DateTime.Now, EndpointInID = endpointId };
            employee.WorkRegisterEvents.Add(workRegisterEvent);
            return Update(employee) > 0;
        }

        public bool RegisterEventOut(int employeeId, int endpointId)
        {
            var employee = GetEntityByID(employeeId);
            var workRegisterEvent = FindLastWorkRegister(employeeId);
            workRegisterEvent.DateGoOut = DateTime.Now;
            workRegisterEvent.EndpointOutID = endpointId;
            return Update(employee) > 0;
        }

        public List<EmployeeViewModel> FilterEmployeeAutoComplete(string filter)
        {
            List<EmployeeViewModel> res = new List<EmployeeViewModel>();
            if (!string.IsNullOrEmpty(filter))
            {
                Expression<Func<Employee, bool>> expr = e => (e.Name + " " + e.Surename).Contains(filter);
                _employeeRepository.FindByCondition(expr).ToList().ForEach(e => res.Add(EntityToViewModel(e)));
            }
            return res;
        }
    }
}
