using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class WorkRegisterEventViewModel : ViewModel, IViewModel
    {
        public int EmployeeID { get; set; }
        public DateTime DateGoIn { get; set; }
        public int EndpointInID { get; set; }
        public string EndpointInName { get; set; }
        public DateTime DateGoOut { get; set; }
        public string EndpointOutName { get; set; }
    }
}
