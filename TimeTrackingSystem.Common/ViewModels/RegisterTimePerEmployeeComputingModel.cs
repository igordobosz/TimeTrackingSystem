using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackingSystem.Common.DTO;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimePerEmployeeComputingModel
    {
        public int DataMonthWorkDays{ get; set; }
        public TimeSpan DataMonthWorkHours { get; set; }
        public TimeSpan StatWorkHours { get; set; }
        public TimeSpan StatOverTimes { get; set; }
        public TimeSpan StatNeededHours { get; set; }
        public TimeSpan SumWorkHours { get; set; }
        public TimeSpan SumOverTimes { get; set; }
    }
}
