using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neosoft.EmployeeManagement.DAL.EFDbFirst
{
    [MetadataType(typeof(CityModel))]
    public partial class City
    {
        
    }
    public class CityModel
    {
        [Display(Name = "City")]
        public string CityName { get; set; }
    }
}
