using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Common.Contracts;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class EmployeeViewModel : ViewModel, IViewModel
    {
        public int? EmployeeGroupID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Surename { get; set; }
        [StringLength(128)]
        public string IdentityCode { get; set; }
    }
}
