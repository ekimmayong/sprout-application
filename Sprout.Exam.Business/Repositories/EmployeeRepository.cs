using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(IApplicationDbContext context): base(context)
        {
            
        }
    }
}
