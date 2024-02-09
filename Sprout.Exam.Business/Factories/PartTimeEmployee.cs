using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public class PartTimeEmployee : ISalaryCalculator
    {
        public async Task<string> CalculateSalary(WorkAndAbsentDaysDto data)
        {
            //Static per hour value
            decimal perHour = (decimal)70;

            // Calculate working hours * rate per hour
            decimal partTimeSalary = data.WorkedHours * perHour;

            return string.Format("{0:0,0.00}", Math.Round(partTimeSalary, 2));
        }
    }
}
