﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Interfaces;
using AutoMapper;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all Employee data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _employeeService.GetAllEmployees();
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(response);

            return Ok(result);
        }

        /// <summary>
        /// Query data by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _employeeService.GetEmployeeById(id);
            var result = _mapper.Map<EmployeeDto>(response);
            return Ok(result);
        }

        /// <summary>
        /// Update Employee data.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditEmployeeDto input)
        {
            var result = _mapper.Map<Employee>(input);
            var item = await _employeeService.UpdateEmployee(id, result);

            return Ok(item);
        }

        /// <summary>
        /// Create new Employee.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var result = _mapper.Map<Employee>(input);
            var response = await _employeeService.CreateNewEmployee(result);

            return Ok(response);
        }


        /// <summary>
        /// Delete Employee data from DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            return Ok(result);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WorkAndAbsentDaysDto"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, WorkAndAbsentDaysDto data)
        {
            var result = await _employeeService.CalculateSalary(id, data);

            return Ok(result);
        }
    }
}
