using Neosoft.EmployeeManagement.DAL.EFDbFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neosoft.EmployeeManagement.BAL.Employee
{
    public interface IEmployeeMasterBAL
    {
        List<EmployeeMaster> GetEmployeeMasters();

        EmployeeMaster GetEmployeeMasterById(int employeeId);

        bool InsertEmployeeMaster(EmployeeMaster employeeMaster);

        bool UpdateEmployeeMaster(EmployeeMaster employeeMaster);

        bool DeleteEmployeeMaster(EmployeeMaster employeeMaster);

        List<Country> GetCountries();

        List<State> GetStates();

        List<City> GetCities();
    }
}
