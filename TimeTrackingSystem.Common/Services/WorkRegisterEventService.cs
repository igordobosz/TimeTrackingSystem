using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.ViewModels;
using TimeTrackingSystem.Data.Models;
using TimeTrackingSystem.Data.Repositories;

namespace TimeTrackingSystem.Common.Services
{
    public class WorkRegisterEventService : ServiceBase<WorkRegisterEvent, WorkRegisterEventViewModel>, IWorkRegisterEventService
    {
        private IRepository<WorkRegisterEvent> _workRegisterRepository;
        private IEmployeeService _employeeService;
        public WorkRegisterEventService(IRepositoryWrapper repositoryWrapper, IMapper mapper, IEmployeeService employeeService) : base(repositoryWrapper, mapper)
        {
            _workRegisterRepository = repositoryWrapper.GetRepository<WorkRegisterEvent>();
            _employeeService = employeeService;
        }

        public RegisterTimePerEmployeeViewModel GetWorkEventsByEmployeeAndDate(int employeeId, DateTime date)
        {
            List<WorkRegisterEventViewModel> workEvents = new List<WorkRegisterEventViewModel>();
            var query = FindAllFinished().Where(e =>
                e.EmployeeID == employeeId && e.DateGoIn.Year == date.Year && e.DateGoIn.Month == date.Month && e.DateGoOut != DateTime.MinValue);
            query.ToList().ForEach(e => workEvents.Add(EntityToViewModel(e)));
            List<RegisterTimePerEmployeeDayWrapperViewModel> workEventDayList  = new List<RegisterTimePerEmployeeDayWrapperViewModel>();
            TimeSpan computedHoursSum = TimeSpan.Zero;
            TimeSpan overTimeHoursSum = TimeSpan.Zero;
            for (int day=1; day <= DateTime.DaysInMonth(date.Year, date.Month); day++)
            {
                var workEventsDay = workEvents.Where(e => e.DateGoIn.Day == day);
                if (workEventsDay.Any())
                {
                    foreach(WorkRegisterEventViewModel workEvent in workEventsDay)
                    {
                        var dayWrapper = new RegisterTimePerEmployeeDayWrapperViewModel();
                        dayWrapper.Day = day;
                        dayWrapper.WorkRegisterEvent = workEvent;
                        CalculateWorkTime(dayWrapper, workEvent);
                        computedHoursSum += dayWrapper.ComputedTime;
                        overTimeHoursSum += dayWrapper.OverTime;
                        workEventDayList.Add(dayWrapper);
                    }
                }
                else
                {
                    var dayWrapper = new RegisterTimePerEmployeeDayWrapperViewModel();
                    dayWrapper.Day = day;
                    workEventDayList.Add(dayWrapper);
                }
            }
            RegisterTimePerEmployeeViewModel ans = new RegisterTimePerEmployeeViewModel();
            ans.WorkEventDayList = new FindByConditionResponse<RegisterTimePerEmployeeDayWrapperViewModel>(){CollectionSize = workEventDayList.Count(), ItemList = workEventDayList };
            ans.SummaryWorkTime = computedHoursSum;
            ans.OverTimes = overTimeHoursSum;
            ans.WorkDays = workEventDayList.Where(e=>e.WorkRegisterEvent!=null).GroupBy(e => e.Day).Count();
            return ans;
        }

        public RegisterTimePerDayViewModel GetWorkEventsByDay(DateTime date)
        {
            List<WorkRegisterEventViewModel> workEvents = new List<WorkRegisterEventViewModel>();
            var query = FindAllFinished().Where(e => e.DateGoIn.Date == date.Date);
            query.ToList().ForEach(e => workEvents.Add(EntityToViewModel(e)));

            List<RegisterTimePerDayEmployeeWrapperViewModel> workEventEmployeeList = new List<RegisterTimePerDayEmployeeWrapperViewModel>();
            foreach (WorkRegisterEventViewModel vm in workEvents)
            {
                var newEmployeeWrapper = new RegisterTimePerDayEmployeeWrapperViewModel();
                newEmployeeWrapper.WorkRegisterEvent = vm;
                var emp = _employeeService.GetByID(vm.EmployeeID);
                newEmployeeWrapper.EmployeeFullName = $"{emp.Name} {emp.Surename}";
                newEmployeeWrapper.EmployeeID = vm.EmployeeID;
                CalculateWorkTime(newEmployeeWrapper, vm);
                workEventEmployeeList.Add(newEmployeeWrapper);
            }
            
            RegisterTimePerDayViewModel ans = new RegisterTimePerDayViewModel();
            ans.WorkEventDayList = new FindByConditionResponse<RegisterTimePerDayEmployeeWrapperViewModel>() { CollectionSize = workEventEmployeeList.Count(), ItemList = workEventEmployeeList };
            return ans;
        }

        private IQueryable<WorkRegisterEvent> FindAllFinished()
        {
            return _workRegisterRepository.FindAll().Where(e => e.DateGoOut != DateTime.MinValue);
        }

        private void CalculateWorkTime(RegisterTimePerWrapper dayWrapper, WorkRegisterEventViewModel workEvent)
        {
            //TODO INSERT TOLERANCJE/EMPLOYEE GROUP USTAWIENIA
            var etatGodziny = 8;
            var tolerancja = 15;

            var dateDiff = workEvent.DateGoOut - workEvent.DateGoIn;
            var computedHours = dateDiff.Hours;
            if (dateDiff.Minutes > 60 - tolerancja)
                computedHours++;

            if (computedHours > etatGodziny)
            {
                dayWrapper.OverTime = TimeSpan.FromHours(computedHours - etatGodziny);
                dayWrapper.ComputedTime = TimeSpan.FromHours(etatGodziny);
            }
            else
            {
                dayWrapper.ComputedTime = TimeSpan.FromHours(computedHours);
                dayWrapper.OverTime = TimeSpan.Zero;
            }
            dayWrapper.NightWork = workEvent.DateGoIn.Day != workEvent.DateGoOut.Day;
        }
    }
}
