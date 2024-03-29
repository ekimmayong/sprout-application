﻿using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Factories;
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
        private readonly IEmployeeFactory _employeeFactory;
        public EmployeeService(IBaseRepository<Employee> employeeRepository, IEmployeeFactory employeeFactory)
        {
            _employeeRepository = employeeRepository;
            _employeeFactory = employeeFactory;
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
            return await _employeeRepository.Find(x => x.Id == id, includeProperties: "EmployeeType").FirstOrDefaultAsync();
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

        public async Task<string> CalculateSalary(int employeeId, WorkAndAbsentDaysDto data)
        {
            var employee = _employeeRepository.Find(x => x.Id == employeeId, includeProperties: "EmployeeType");
            
            if (employee != null)
            {
                var result = employee.First();
                var calculateSalary = _employeeFactory.CreateSalaryCalculator(result.EmployeeType.TypeName);
                return await calculateSalary.CalculateSalary(data);
            }

            return "Employee not found";
        }
    }
}
