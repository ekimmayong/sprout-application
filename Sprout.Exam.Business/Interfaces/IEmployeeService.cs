using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> CreateNewEmployee(Employee employee);
        Task<string> DeleteEmployee(int id);
        Task<string> UpdateEmployee(int id, Employee employee);
        Task<object> CalculateSalary(int employeeId, decimal absentDays, decimal workedDays);
    }
}
