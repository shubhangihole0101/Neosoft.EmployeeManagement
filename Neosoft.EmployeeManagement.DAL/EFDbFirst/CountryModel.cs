using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neosoft.EmployeeManagement.DAL.EFDbFirst
{
    [MetadataType(typeof(CountryModel))]
    public partial class Country
    {
        
    }

    public class CountryModel
    {
        [Display(Name = "Country")]
        public string CountryName { get; set; }
    }
}
