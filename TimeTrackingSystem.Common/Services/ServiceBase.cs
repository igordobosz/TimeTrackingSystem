using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Extensions;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Common.Services
{
    public abstract class ServiceBase<T, VM> : IServiceBase<VM> where T : Entity where VM : IViewModel
    {
        //TODO: zamiast class zrobic IEntity
        protected readonly IRepositoryWrapper _repositoryWrapper;
        protected readonly IMapper _mapper;
        public ServiceBase(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._mapper = mapper;
        }

        public List<VM> FindAll()
        {
            List<VM> res = new List<VM>();
            _repositoryWrapper.GetRepository<T>().FindAll().ToList().ForEach(e => res.Add(EntityToViewModel(e)));
            return res;
        }

        public FindByConditionResponse<VM> FindByConditions(int pageIndex, int pageSize, string searchExpression, string sortColumn, string sortOrder)
        {
            List<VM> res = new List<VM>();
            _repositoryWrapper.GetRepository<T>().FindAll().Paged(pageIndex, pageSize).ToList().ForEach(e => res.Add(EntityToViewModel(e)));
            int listSize = _repositoryWrapper.GetRepository<T>().FindAll().Count();
            return new FindByConditionResponse<VM>() {ItemList = res, CollectionSize = listSize};
        }

        public VM GetByID(int id)
        {
            return EntityToViewModel(_repositoryWrapper.GetRepository<T>().FindAll().FirstOrDefault(e => e.ID == id));
        }

        public int Insert(VM item)
        {
            var entity = ViewModelToEntity(item);
            _repositoryWrapper.GetRepository<T>().Insert(entity);
            return _repositoryWrapper.SaveChanges() ? entity.ID : -1;
        }

        public int Update(VM item)
        {
            var entity = ViewModelToEntity(item);
            _repositoryWrapper.GetRepository<T>().Update(ViewModelToEntity(item));
            return _repositoryWrapper.SaveChanges() ? entity.ID : -1;
        }

        public int Delete(int id)
        {
            return Delete(GetByID(id));
        }

        public int Delete(VM item)
        {
            var entity = ViewModelToEntity(item);
            _repositoryWrapper.GetRepository<T>().Delete(ViewModelToEntity(item));
            return _repositoryWrapper.SaveChanges() ? entity.ID : -1;
        }

        private VM EntityToViewModel(T entity)
        {
            return _mapper.Map<VM>(entity);
        }

        private T ViewModelToEntity(VM viewModel)
        {
            return _mapper.Map<T>(viewModel);
        }
    }
}
