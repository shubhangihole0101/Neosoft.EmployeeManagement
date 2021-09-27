using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neosoft.EmployeeManagement.DAL.EFDbFirst
{
    [MetadataType(typeof(EmployeeMasterModel))]
    public partial class EmployeeMaster
    {
    }

    public class EmployeeMasterModel
    {
        public int Row_Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last Name")]
        public string LastName { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please select a country")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Please select a state")]
        public int StateId { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please select a city")]
        public int CityId { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter email")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [RemoteClientServerAttribute("IsEmailExits", "EmployeeMasters", AdditionalFields = "Row_Id", ErrorMessage = "Email already exists")]
        public string EmailAddress { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [RemoteClientServerAttribute("IsMobileNumberExits", "EmployeeMasters", AdditionalFields = "Row_Id", ErrorMessage = "Mobile Number already exists")]
        public string MobileNumber { get; set; }

        [Display(Name = "PAN number")]
        [Required(ErrorMessage = "Please enter PAN number")]
        [RegularExpression("^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Invalid PAN Number")]
        [RemoteClientServerAttribute("IsPanNumberExits", "EmployeeMasters", AdditionalFields = "Row_Id", ErrorMessage = "Pan Number already exists")]
        public string PanNumber { get; set; }

        [Display(Name = "Passport Number")]
        [Required(ErrorMessage = "Please enter passport ")]
        [RegularExpression(@"^([A-Z a-z]){1}([0-9]){7}$", ErrorMessage = "Please enter correct passport")]
        [RemoteClientServerAttribute("IsPassportNumberExits", "EmployeeMasters", AdditionalFields = "Row_Id", ErrorMessage = "Passport Number already exists")]
        public string PassportNumber { get; set; }

        public string ProfileImage { get; set; }

        public byte Gender { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please enter Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateValidationAttribute(ErrorMessage = "Date of birth should be greater than 18 year")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date Of Joinee")]
        [Required(ErrorMessage = "Please enter Date of joinee")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfJoinee { get; set; }
    }
}
