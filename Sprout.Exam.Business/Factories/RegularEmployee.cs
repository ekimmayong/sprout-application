using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public class RegularEmployee : ISalaryCalculator
    {
        public async Task<string> CalculateSalary(WorkAndAbsentDaysDto data)
        {
            //Static Monthy salary for probationary
            decimal regularMonthlySalary = (decimal)20000.00;

            //Calculate Net salary = Basic salary less absences and tax
            decimal regularSalary = regularMonthlySalary - ((regularMonthlySalary / 22) * data.AbsentDays) - (regularMonthlySalary * (decimal)0.12);
            return string.Format("{0:0,0.00}", Math.Round(regularSalary, 2));
        }
    }
}
