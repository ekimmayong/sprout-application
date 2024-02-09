using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.DataAccess.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Route("api/type")]
    [ApiController]
    [Authorize]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IEmployeeTypeService _employeeTypeService;
        private readonly IMapper _mapper;
        public EmployeeTypeController(IEmployeeTypeService employeeTypeService, IMapper mapper)
        {
            _employeeTypeService = employeeTypeService;
            _mapper = mapper;
        }

        [HttpGet("get-types")]
        public async Task<IActionResult> GetEmployeeTypes()
        {
            var response = await _employeeTypeService.GetEmployeeType();
            var items = _mapper.Map<IEnumerable<EmployeeTypeDto>>(response);

            return Ok(items);
        }

        [HttpPost("add-type")]
        public async Task<IActionResult> AddEmployeeType(EmployeeTypeDto type)
        {
            var data = _mapper.Map<EmployeeType>(type);
            var response = await _employeeTypeService.CreateNewEmployeeType(data);

            return Ok(response);
        }
    }
}
