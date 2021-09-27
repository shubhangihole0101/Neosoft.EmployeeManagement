using Neosoft.EmployeeManagement.DAL.DAL;
using Neosoft.EmployeeManagement.DAL.EFDbFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neosoft.EmployeeManagement.BAL.Employee
{
    public class EmployeeMasterBAL : IEmployeeMasterBAL
    {
        private IEmployeeMasterDAL _employeeMasterDAL;

        public EmployeeMasterBAL(IEmployeeMasterDAL employeeMasterDAL)
        {
            _employeeMasterDAL = employeeMasterDAL;
        }

        public bool DeleteEmployeeMaster(EmployeeMaster employeeMaster)
        {
            return _employeeMasterDAL.DeleteEmployeeMaster(employeeMaster);
        }

        public List<City> GetCities()
        {
            return _employeeMasterDAL.GetCities();
        }

        public List<Country> GetCountries()
        {
            return _employeeMasterDAL.GetCountries();
        }

        public EmployeeMaster GetEmployeeMasterById(int employeeId)
        {
            return _employeeMasterDAL.GetEmployeeMasterById(employeeId);
        }

        public List<EmployeeMaster> GetEmployeeMasters()
        {
            return _employeeMasterDAL.GetEmployeeMasters();
        }

        public List<State> GetStates()
        {
            return _employeeMasterDAL.GetStates();
        }

        public bool InsertEmployeeMaster(EmployeeMaster employeeMaster)
        {
            return _employeeMasterDAL.InsertEmployeeMaster(employeeMaster);
        }

        public bool UpdateEmployeeMaster(EmployeeMaster employeeMaster)
        {
            return _employeeMasterDAL.UpdateEmployeeMaster(employeeMaster);
        }
    }
}
