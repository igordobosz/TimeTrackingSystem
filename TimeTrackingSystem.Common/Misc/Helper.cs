﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackingSystem.Common.Misc
{
    public static class Helper
    {
        public static string GenerateRandomToken()
        {
            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            return guidString;
        }

        public static string FormatTimeSpan(TimeSpan date)
        {
            return $"{(int) date.TotalHours} hours {date.Minutes} minutes";
        }
    }
}
