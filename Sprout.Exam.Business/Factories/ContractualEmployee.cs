using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public class ContractualEmployee : ISalaryCalculator
    {
        public async Task<string> CalculateSalary(WorkAndAbsentDaysDto data)
        {
            decimal dailySalary = (decimal)500.00;
            decimal contractualSalary = data.WorkedDays * dailySalary;
            return string.Format("{0:0,0.00}", Math.Round(contractualSalary, 2));
        }
    }
}
