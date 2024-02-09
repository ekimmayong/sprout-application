using Sprout.Exam.DataAccess.Data;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.Business.Repositories
{
    public class EmployeeTypeRepository: BaseRepository<EmployeeType>
    {
        public EmployeeTypeRepository(IApplicationDbContext context): base(context)
        {
            
        }
    }
}
