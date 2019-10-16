using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackingSystem.Common.Misc
{
    public static class EndpointTokenHelper
    {
        public static string GenerateRandomToken()
        {
            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            return guidString;
        }
    }
}
