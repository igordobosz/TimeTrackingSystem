using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimePerEmployeeDayWrapperViewModel
    {
        public int Day { get; set; }
        public TimeSpan ComputedTime { get; set; }
        public TimeSpan OverTime { get; set; }
        public bool NightWork { get; set; }
        public WorkRegisterEventViewModel WorkRegisterEvent { get; set; }
    }
}
