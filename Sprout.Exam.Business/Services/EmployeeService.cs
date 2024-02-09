using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Repositories;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepository<Employee> _employeeRepository;
        public EmployeeService(IBaseRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateNewEmployee(Employee employee)
        {
            var response = await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();

            return response;
        }

        public async Task<string> DeleteEmployee(int id)
        {
            var item = await _employeeRepository.GetByIdAsync(id);
            if (item != null)
            {
                await _employeeRepository.DeleteAsync(item);
                await _employeeRepository.SaveChangesAsync();

                return "Deleted successfully";
            }

            return "Item not found";
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var response = await _employeeRepository.GetAllAsync(includeProperties: "EmployeeType");
            return response;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.FindAsync(x => x.Id == id, includeProperties: "EmployeeType").Result.FirstOrDefaultAsync();
        }

        public async Task<string> UpdateEmployee(int id, Employee employee)
        {
            var item = await _employeeRepository.GetByIdAsync(id);
            if (item != null)
            {
                item.FullName = employee.FullName;
                item.TIN = employee.TIN;
                item.Birthdate = employee.Birthdate;
                item.IsDeleted = employee.IsDeleted;
                item.EmployeeTypeId = employee.EmployeeTypeId;

                await _employeeRepository.UpdateAsync(item);
                await _employeeRepository.SaveChangesAsync();

                return "Update successfully";
            }

            return "No update performed";
        }

        public async Task<object> CalculateSalary(int employeeId, WorkAndAbsentDaysDto data)
        {
            var employee = await _employeeRepository.FindAsync(x => x.Id == employeeId, includeProperties: "EmployeeType").Result.FirstOrDefaultAsync();
            decimal regularMonthlySalary = (decimal)20000.00;
            decimal probationaryMonthlySalary = (decimal)18500.00;
            decimal dailySalary = (decimal)500.00;
            decimal perHour = (decimal)70;

            if (employee != null)
            {
                switch (employee.EmployeeType.TypeName)
                {
                    case "Regular":
                        // calculate regular salary with less absent and less 12% tax
                        var regularSalary = regularMonthlySalary - ((regularMonthlySalary / 22) * data.AbsentDays) - (regularMonthlySalary * (decimal)0.12);

                        //Format value to 12,345.67
                        var value = string.Format("{0:0,0.00}", Math.Round(regularSalary, 2));
                        return value;

                    case "Contractual":
                        //Calculate Contractual salary. Total working days * daily salary fo 500
                        var contractualSalary = data.WorkedDays * dailySalary;

                        //Format value to 12,345.67
                        return string.Format("{0:0,0.00}", Math.Round(contractualSalary, 2));

                    case "Probationary":
                        var probationarySalary = probationaryMonthlySalary - ((probationaryMonthlySalary / 22) * data.AbsentDays) - (probationaryMonthlySalary * (decimal)0.10);

                        //Format value to 12,345.67
                        return string.Format("{0:0,0.00}", Math.Round(probationarySalary, 2));

                    case "Part Time":
                        var partTimeSalary = data.WorkedHours * perHour;

                        //Format value to 12,345.67
                        return string.Format("{0:0,0.00}", Math.Round(partTimeSalary));

                    default:
                        return "Employee Type not found";
                }
            }
            return "Employee not found";
        }
    }
}
