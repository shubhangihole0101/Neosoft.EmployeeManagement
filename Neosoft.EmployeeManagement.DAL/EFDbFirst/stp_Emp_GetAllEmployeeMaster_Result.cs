//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Neosoft.EmployeeManagement.DAL.EFDbFirst
{
    using System;
    
    public partial class stp_Emp_GetAllEmployeeMaster_Result
    {
        public int Row_Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string PanNumber { get; set; }
        public string PassportNumber { get; set; }
        public string ProfileImage { get; set; }
        public byte Gender { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public System.DateTime DateOfJoinee { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
