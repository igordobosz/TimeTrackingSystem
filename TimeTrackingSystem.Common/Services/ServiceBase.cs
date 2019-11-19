using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Extensions;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Data.Contracts;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public abstract class ServiceBase<T, VM> : IServiceBase<VM> where T : Entity where VM : ViewModel
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
            _repositoryWrapper.GetRepository<T>().FindAll().AsNoTracking().ToList().ForEach(e => res.Add(EntityToViewModel(e)));
            return res;
        }

        public FindByConditionResponse<VM> FindByConditions(int pageIndex, int pageSize, string searchExpression, string sortColumn, string sortOrder)
        {
            VM searchVM = null;
            if (!string.IsNullOrEmpty(searchExpression))
            {
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                searchVM = JsonConvert.DeserializeObject<VM>(searchExpression);
            }
            List<VM> res = new List<VM>();
            var query = _repositoryWrapper.GetRepository<T>().FindAll().SortByProperty(sortColumn, sortOrder)
                .Paged(pageIndex, pageSize);
            ApplyFilters(ref query, searchVM);
            query.ToList().ForEach(e => res.Add(EntityToViewModel(e)));
            int listSize = res.Count();
            return new FindByConditionResponse<VM>() {ItemList = res, CollectionSize = listSize};
        }

        protected virtual void ApplyFilters(ref IQueryable<T> query, VM searchModel)
        {

        }

        public VM GetByID(int id)
        {
            return EntityToViewModel(_repositoryWrapper.GetRepository<T>().GetByID(id));
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
            _repositoryWrapper.GetRepository<T>().Delete(id);
            return _repositoryWrapper.SaveChanges() ? 1 : -1;
        }

        public int Delete(VM item)
        {
            var entity = ViewModelToEntity(item);
            _repositoryWrapper.GetRepository<T>().Delete(ViewModelToEntity(item));
            return _repositoryWrapper.SaveChanges() ? entity.ID : -1;
        }

        protected T GetEntityByID(int id)
        {
            return _repositoryWrapper.GetRepository<T>().GetByID(id);
        }

        protected int Update(T item)
        {
            _repositoryWrapper.GetRepository<T>().Update(item);
            return _repositoryWrapper.SaveChanges() ? item.ID : -1;
        }

        protected VM EntityToViewModel(T entity)
        {
            return _mapper.Map<VM>(entity);
        }

        protected T ViewModelToEntity(VM viewModel)
        {
            var entity = GetEntityByID(viewModel.ID);
            if (entity != null)
            {
                _mapper.Map(viewModel, entity);
            }
            else
            {
                entity = _mapper.Map<T>(viewModel);
            }

            return entity;
        }
    }
}
