using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neosoft.EmployeeManagement.DAL.EFDbFirst
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            DateTime lastDateFor18 = DateTime.Now.AddYears(-18);
            return todayDate <= lastDateFor18;
        }
    }
}
