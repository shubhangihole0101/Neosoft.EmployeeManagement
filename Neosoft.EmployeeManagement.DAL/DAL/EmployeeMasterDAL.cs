using Neosoft.EmployeeManagement.DAL.EFDbFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Neosoft.EmployeeManagement.DAL.DAL
{
    public class EmployeeMasterDAL : IEmployeeMasterDAL
    {
        private NeoSoft_ShubhangiHoleEntities db = new NeoSoft_ShubhangiHoleEntities();

        public bool DeleteEmployeeMaster(EmployeeMaster employeeMaster)
        {
            db.stp_Emp_DeleteEmployeeMaster(employeeMaster.Row_Id);
            return true;
        }

        public List<City> GetCities()
        {
            return db.Cities.ToList();
        }

        public List<Country> GetCountries()
        {
            return db.Countries.ToList();
        }

        public EmployeeMaster GetEmployeeMasterById(int employeeId)
        {
            return db.EmployeeMasters.FirstOrDefault(t => t.Row_Id.Equals(employeeId));
        }

        public List<EmployeeMaster> GetEmployeeMasters()
        {
            return db.EmployeeMasters.Include(t => t.City).Include(t => t.State).Include(t => t.Country)
                .Where(t=>!t.IsDeleted).ToList();
        }

        public List<State> GetStates()
        {
            return db.States.ToList();
        }

        public bool InsertEmployeeMaster(EmployeeMaster employeeMaster)
        {
            employeeMaster.CreatedDate = DateTime.Now;
            db.EmployeeMasters.Add(employeeMaster);
            db.SaveChanges();
            return true;
        }

        public bool UpdateEmployeeMaster(EmployeeMaster employeeMaster)
        {
            db.Entry(employeeMaster).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
    }
}
