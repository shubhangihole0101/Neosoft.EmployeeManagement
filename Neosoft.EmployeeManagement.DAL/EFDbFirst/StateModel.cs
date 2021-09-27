using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neosoft.EmployeeManagement.DAL.EFDbFirst
{
    [MetadataType(typeof(StateModel))]
    public partial class State
    {

    }

    public class StateModel
    {
        [Display(Name = "State")]
        public string StateName { get; set; }
    }
}
