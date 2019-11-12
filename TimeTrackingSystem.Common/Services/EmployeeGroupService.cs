using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public class EmployeeGroupService : ServiceBase<EmployeeGroup, EmployeeGroupViewModel>, IEmployeeGroupService
    {
        private IRepository<EmployeeGroup> _baseRepository;
        public EmployeeGroupService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper)
        {
            _baseRepository = repositoryWrapper.GetRepository<EmployeeGroup>();
        }

        public List<EmployeeGroupCbxViewModel> ListCbx()
        {
            List<EmployeeGroupCbxViewModel> res = new List<EmployeeGroupCbxViewModel>();
            _baseRepository.FindAll().AsNoTracking().ToList().ForEach(e => res.Add(_mapper.Map<EmployeeGroupCbxViewModel>(e)));
            return res;
        }
    }
}
