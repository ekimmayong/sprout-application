using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeTypeService
    {
        Task<IEnumerable<EmployeeType>> GetEmployeeType();
        Task<EmployeeType> CreateNewEmployeeType(EmployeeType type);
    }
}
