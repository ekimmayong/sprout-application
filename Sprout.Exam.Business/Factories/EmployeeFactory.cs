using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public class EmployeeFactory: IEmployeeFactory
    {
        public ISalaryCalculator CreateSalaryCalculator(string employeeType)
        {
            return employeeType.ToLower() switch
            {
                "regular" => new RegularEmployee(),
                "probationary" => new ProbationaryEmployee(),
                "contractual" => new ContractualEmployee(),
                "part time" => new PartTimeEmployee(),
                _ => throw new ArgumentException("Invalid employee type"),
            };
        }
    }
}
