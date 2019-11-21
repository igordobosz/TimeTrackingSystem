using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Common.Contracts;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class EmployeeGroupCbxViewModel : ViewModel, IViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
    }
}
