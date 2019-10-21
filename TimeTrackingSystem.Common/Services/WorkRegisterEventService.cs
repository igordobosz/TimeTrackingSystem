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
        public WorkRegisterEventService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper)
        {
            _workRegisterRepository = repositoryWrapper.GetRepository<WorkRegisterEvent>();
        }

        public RegisterTimePerEmployeeViewModel GetWorkEventsByEmployeeAndDate(int employeeId, DateTime date)
        {
            List<WorkRegisterEventViewModel> workEvents = new List<WorkRegisterEventViewModel>();
            var query = _workRegisterRepository.FindAll().Where(e =>
                e.EmployeeID == employeeId && e.DateGoIn.Year == date.Year && e.DateGoOut.Month == date.Month);
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
                        dayWrapper.NightWork = workEvent.DateGoIn.Day != workEvent.DateGoOut.Day;
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

        private void CalculateWorkTime(RegisterTimePerEmployeeDayWrapperViewModel dayWrapper, WorkRegisterEventViewModel workEvent)
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
        }

        private bool IsInTollerance(TimeSpan time)
        {
            var tolerancja = 15;
            return time.Minutes < tolerancja || 60 - tolerancja < time.Minutes;
        }
    }
}
