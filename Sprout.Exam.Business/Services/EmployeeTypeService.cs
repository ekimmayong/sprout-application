using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IBaseRepository<EmployeeType> _employeeTypeRepository;
        public EmployeeTypeService(IBaseRepository<EmployeeType> employeeTypeRepository) 
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        public async Task<EmployeeType> CreateNewEmployeeType(EmployeeType type)
        {
            var response = await _employeeTypeRepository.AddAsync(type);
            await _employeeTypeRepository.SaveChangesAsync();

            return response;
        }

        public async Task<IEnumerable<EmployeeType>> GetEmployeeType()
        {
            var response = await _employeeTypeRepository.GetAllAsync();

            return response;
        }
    }
}
