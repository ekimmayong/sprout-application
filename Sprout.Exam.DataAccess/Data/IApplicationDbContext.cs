using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<EmployeeType> EmployeeTypes { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
