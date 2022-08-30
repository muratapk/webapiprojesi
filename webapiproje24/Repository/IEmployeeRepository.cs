using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
namespace webapiproje24.Repository
{
  public  interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> Search(string name);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int Id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee,int Id);
        Task<Employee> DeleteEmployee(int Id);


    }
}
