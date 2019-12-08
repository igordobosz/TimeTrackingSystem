using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nager.Date;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Common.DTO;
using TimeTrackingSystem.Common.Misc;
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

        public RegisterTimePerEmployeeViewModel GetWorkEventsByEmployeeAndDate(int employeeId, DateTime date, bool isSumOvertimes, int tolerance)
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
                        dayWrapper.IsSaturday = new DateTime(date.Year, date.Month, day).DayOfWeek == DayOfWeek.Saturday;
                        dayWrapper.IsSunday = new DateTime(date.Year, date.Month, day).DayOfWeek == DayOfWeek.Sunday || DateSystem.IsPublicHoliday(new DateTime(date.Year, date.Month, day), CountryCode.PL);
                        dayWrapper.WorkRegisterEvent = workEvent;
                        CalculateWorkTime(dayWrapper, workEvent, tolerance);
                        computedHoursSum += dayWrapper.ComputedTime;
                        overTimeHoursSum += dayWrapper.OverTime;
                        workEventDayList.Add(dayWrapper);
                    }
                }
                else
                {
                    var dayWrapper = new RegisterTimePerEmployeeDayWrapperViewModel();
                    dayWrapper.Day = day;
                    dayWrapper.IsSaturday = new DateTime(date.Year, date.Month, day).DayOfWeek == DayOfWeek.Saturday;
                    dayWrapper.IsSunday = new DateTime(date.Year, date.Month, day).DayOfWeek == DayOfWeek.Sunday || DateSystem.IsPublicHoliday(new DateTime(date.Year, date.Month, day), CountryCode.PL);
                    workEventDayList.Add(dayWrapper);
                }
            }

            var empGroup = _employeeRepository.GetByID(employeeId).EmployeeGroup;
            RegisterTimePerEmployeeComputingModel comput = new RegisterTimePerEmployeeComputingModel();
            comput.DataMonthWorkDays = GetWorkingDaysOfMonth(date);
            comput.DataMonthWorkHours = TimeSpan.FromHours(comput.DataMonthWorkDays * 8);
            if (empGroup != null)
            {
                comput.StatNeededHours = TimeSpan.FromHours(((double) empGroup.WorkingHoursPerWeek / 100) * comput.DataMonthWorkHours.TotalHours);
            }
            else
            {
                comput.StatNeededHours = TimeSpan.FromHours(comput.DataMonthWorkHours.TotalHours);
            }
            
            comput.StatWorkHours = computedHoursSum;
            comput.StatOverTimes = overTimeHoursSum;
            comput.SumWorkHours = comput.StatWorkHours;
            comput.SumOverTimes = comput.StatOverTimes;
            var diff = comput.StatNeededHours - comput.StatWorkHours;
            if (diff > TimeSpan.Zero)
            {
                if (isSumOvertimes)
                {

                    if (comput.SumOverTimes >= diff)
                    {
                        comput.SumWorkHours = comput.StatNeededHours;
                        comput.SumOverTimes -= diff;
                    }
                    else
                    {
                        comput.SumWorkHours += comput.StatOverTimes;
                        comput.SumOverTimes = TimeSpan.Zero;
                    }
                }
            }
            else
            {
                comput.SumOverTimes += (comput.StatWorkHours - comput.StatNeededHours);
                comput.SumWorkHours = comput.StatNeededHours;
            }



            RegisterTimePerEmployeeViewModel ans = new RegisterTimePerEmployeeViewModel();
            ans.WorkEventDayList = new FindByConditionResponse<RegisterTimePerEmployeeDayWrapperViewModel>() { CollectionSize = workEventDayList.Count(), ItemList = workEventDayList };
            ans.DataMonthWorkDays = comput.DataMonthWorkDays;
            ans.DataMonthWorkHours = Helper.FormatTimeSpan(comput.DataMonthWorkHours);
            ans.StatWorkHours = Helper.FormatTimeSpan(comput.StatWorkHours);
            ans.StatOverTimes = Helper.FormatTimeSpan(comput.StatOverTimes);
            ans.StatNeededHours = Helper.FormatTimeSpan(comput.StatNeededHours);
            ans.SumWorkHours = Helper.FormatTimeSpan(comput.SumWorkHours);
            ans.SumOverTimes = Helper.FormatTimeSpan(comput.SumOverTimes);

            return ans;
        }

        private int GetWorkingDaysOfMonth(DateTime date)
        {
            DayOfWeek[] weekends = { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var remainingDates = Enumerable.Range(1, DateTime.DaysInMonth(date.Year, date.Month))
                .Select(day => new DateTime(date.Year, date.Month, day));
            return remainingDates.Count(e => !weekends.Contains(e.DayOfWeek) && !DateSystem.IsPublicHoliday(e, CountryCode.PL));
        }

        public RegisterTimePerDayViewModel GetWorkEventsByDay(DateTime date, int tolerance, bool findAllFinished = true)
        {
            List<WorkRegisterEventViewModel> workEvents = new List<WorkRegisterEventViewModel>();
            var query = FindAllUnfinished();
            if (findAllFinished)
            {
                query = FindAllFinished();
            }
            query = query.Where(e => e.DateGoIn.Date == date.Date);
            query.ToList().ForEach(e => workEvents.Add(EntityToViewModel(e)));

            List<RegisterTimePerDayEmployeeWrapperViewModel> workEventEmployeeList = new List<RegisterTimePerDayEmployeeWrapperViewModel>();
            foreach (WorkRegisterEventViewModel vm in workEvents)
            {
                var newEmployeeWrapper = new RegisterTimePerDayEmployeeWrapperViewModel();
                newEmployeeWrapper.WorkRegisterEvent = vm;
                var emp = _employeeService.GetByID(vm.EmployeeID);
                newEmployeeWrapper.EmployeeFullName = $"{emp.Name} {emp.Surename}";
                newEmployeeWrapper.EmployeeID = vm.EmployeeID;
                CalculateWorkTime(newEmployeeWrapper, vm, tolerance);
                workEventEmployeeList.Add(newEmployeeWrapper);
            }
            
            RegisterTimePerDayViewModel ans = new RegisterTimePerDayViewModel();
            ans.WorkEventDayList = new FindByConditionResponse<RegisterTimePerDayEmployeeWrapperViewModel>() { CollectionSize = workEventEmployeeList.Count(), ItemList = workEventEmployeeList };
            return ans;
        }

        public RegisterTimePerDayViewModel GetEmployeesInWork()
        {
            return GetWorkEventsByDay(DateTime.Now, 0, false);
        }

        private IQueryable<WorkRegisterEvent> FindAllFinished()
        {
            return _workRegisterRepository.FindAll().Where(e => e.DateGoOut != DateTime.MinValue);
        }

        private IQueryable<WorkRegisterEvent> FindAllUnfinished()
        {
            return _workRegisterRepository.FindAll().Where(e => e.DateGoOut == DateTime.MinValue);
        }

        private void CalculateWorkTime(RegisterTimePerWrapper dayWrapper, WorkRegisterEventViewModel workEvent, int tolerance)
        {
            var etatGodziny = 8;

            var dateDiff = workEvent.DateGoOut - workEvent.DateGoIn;
            var computedHours = dateDiff.Hours;
            if (dateDiff.Minutes > ( 60 - tolerance))
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
