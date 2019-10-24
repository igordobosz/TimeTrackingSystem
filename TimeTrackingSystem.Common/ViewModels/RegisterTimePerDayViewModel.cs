using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.DTO;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimePerDayViewModel
    {
        public FindByConditionResponse<RegisterTimePerDayEmployeeWrapperViewModel> WorkEventDayList { get; set; }
    }

    public abstract class RegisterTimePerWrapper
    {
        public TimeSpan ComputedTime { get; set; }
        public TimeSpan OverTime { get; set; }
        public bool NightWork { get; set; }
    }

    public class RegisterTimePerDayEmployeeWrapperViewModel : RegisterTimePerWrapper
    {
        public int EmployeeID { get; set; }
        public string EmployeeFullName { get; set; }

        public WorkRegisterEventViewModel WorkRegisterEvent { get; set; }
    }
}
