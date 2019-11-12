﻿using System;
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
        private IRepository<Employee> _employeeRepository;
        private IEmployeeService _employeeService;
        public WorkRegisterEventService(IRepositoryWrapper repositoryWrapper, IMapper mapper, IEmployeeService employeeService) : base(repositoryWrapper, mapper)
        {
            _workRegisterRepository = repositoryWrapper.GetRepository<WorkRegisterEvent>();
            _employeeRepository = repositoryWrapper.GetRepository<Employee>();
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

            var empGroup = _employeeRepository.GetByID(employeeId).EmployeeGroup;
            RegisterTimePerEmployeeViewModel ans = new RegisterTimePerEmployeeViewModel();
            ans.WorkEventDayList = new FindByConditionResponse<RegisterTimePerEmployeeDayWrapperViewModel>(){CollectionSize = workEventDayList.Count(), ItemList = workEventDayList };
            ans.DataMonthWorkDays = GetWorkingDaysOfMonth(date);
            ans.DataMonthWorkHours = TimeSpan.FromHours(ans.DataMonthWorkDays * 8);
            if (empGroup != null)
            {
                ans.StatNeededHours = TimeSpan.FromHours(empGroup.WorkingHoursPerWeek / 100 * ans.DataMonthWorkHours.TotalHours);
            }
            else
            {
                ans.StatNeededHours = TimeSpan.FromHours(ans.DataMonthWorkHours.TotalHours);
            }
            
            ans.StatWorkHours = computedHoursSum;
            ans.StatOverTimes = overTimeHoursSum;
            ans.SumWorkHours = ans.StatWorkHours;
            ans.SumOverTimes = ans.StatOverTimes;
            var diff = ans.StatNeededHours - ans.StatWorkHours;
            if (diff > TimeSpan.Zero)
            {
                if (ans.SumOverTimes >= diff)
                {
                    ans.SumWorkHours = ans.StatNeededHours;
                    ans.SumOverTimes -= diff;
                }
                else
                {
                    ans.SumWorkHours += ans.StatOverTimes;
                    ans.SumOverTimes = TimeSpan.Zero;
                }
            }
            else
            {
                ans.SumOverTimes += (ans.StatNeededHours - ans.StatWorkHours);
                ans.SumWorkHours = ans.StatNeededHours;
            }
            return ans;
        }

        private int GetWorkingDaysOfMonth(DateTime date)
        {
            DayOfWeek[] weekends = { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var remainingDates = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month))
                .Select(day => new DateTime(date.Year, date.Month, day));
            return remainingDates.Count(e => !weekends.Contains(e.DayOfWeek));
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
