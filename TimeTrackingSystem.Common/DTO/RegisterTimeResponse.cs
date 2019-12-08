using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackingSystem.Common.DTO
{
    public class RegisterTimeResponse
    {
        public RegisterTimeResponseType ResponseType { get; set; }
        public string WorkTime { get; set; }
        public string EntranceTime { get; set; }
    }

    public enum RegisterTimeResponseType
    {
        SuccessEntrance,
        SuccessLeave,
        InWork,
        OutWork,
        Error
    }
}
