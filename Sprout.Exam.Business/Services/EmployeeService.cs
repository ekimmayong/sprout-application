﻿using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Repositories;
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
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
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

        public async Task<object> CalculateSalary(int employeeId, decimal absentDays, decimal workedDays)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            decimal monthlySalary = (decimal)20000.00;
            decimal dailySalary = (decimal)500.00;

            if (employee != null)
            {
                switch (employee.EmployeeTypeId)
                {
                    case 1:
                        var regularSalary = monthlySalary - ((monthlySalary / 22) * absentDays) - (monthlySalary * (decimal)0.12);
                        return regularSalary;

                    case 2:
                        var contractualSalary = workedDays * dailySalary;
                        return contractualSalary;

                    default:
                        return "Employee Type not found";
                }
            }
            return "Employee not found";
        }
    }
}
