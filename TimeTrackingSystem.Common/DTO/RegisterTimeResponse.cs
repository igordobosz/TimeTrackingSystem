using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackingSystem.Common.DTO
{
    public class RegisterTimeResponse
    {
        public RegisterTimeResponseType ResponseType { get; set; }
        public TimeSpan WorkTime { get; set; }
    }

    public enum RegisterTimeResponseType
    {
        Success,
        InWork,
        OutWork,
        Error
    }
}
