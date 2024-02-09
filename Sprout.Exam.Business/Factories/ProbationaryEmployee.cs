using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public class ProbationaryEmployee : ISalaryCalculator
    {
        public async Task<string> CalculateSalary(WorkAndAbsentDaysDto data)
        {
            //Static Monthy salary for probationary
            decimal probationaryMonthlySalary = (decimal)18500.00;

            //Calculate Net salary = Basic salary less absences and tax
            decimal probationarySalary = probationaryMonthlySalary - ((probationaryMonthlySalary / 22) * data.AbsentDays) - (probationaryMonthlySalary * (decimal)0.10);
            return string.Format("{0:0,0.00}", Math.Round(probationarySalary, 2));
        }
    }
}
