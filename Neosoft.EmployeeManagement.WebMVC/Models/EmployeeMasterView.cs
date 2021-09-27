using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neosoft.EmployeeManagement.WebMVC.Models
{
    public class EmployeeMasterView
    {
        public int Row_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string PanNumber { get; set; }
        public string PassportNumber { get; set; }
        public string ProfileImage { get; set; }

        public string Gender { get; set; }

        public string IsActive { get; set; }
    }
}