using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.DTO;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimePerEmployeeViewModel
    {
        public int DataMonthWorkDays{ get; set; }
        public string DataMonthWorkHours { get; set; }
        public string StatWorkHours { get; set; }
        public string StatOverTimes { get; set; }
        public string StatNeededHours { get; set; }
        public string SumWorkHours { get; set; }
        public string SumOverTimes { get; set; }
        public FindByConditionResponse<RegisterTimePerEmployeeDayWrapperViewModel> WorkEventDayList { get; set; }
    }

    public class RegisterTimePerEmployeeDayWrapperViewModel : RegisterTimePerWrapper
    {
        public int Day { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        public WorkRegisterEventViewModel WorkRegisterEvent { get; set; }
    }
}
