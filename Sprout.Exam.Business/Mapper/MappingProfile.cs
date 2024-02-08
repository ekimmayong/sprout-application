using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(x => x.Birthdate, options => options.MapFrom(c => c.Birthdate.ToString("MM/dd/yyyy hh:mm tt")))
                .ForMember(x => x.TypeId, options => options.MapFrom(c => c.EmployeeTypeId));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(x => x.Birthdate, options => options.MapFrom(c => DateTime.Parse(c.Birthdate)))
                .ForMember(x => x.EmployeeTypeId, options => options.MapFrom(c => c.TypeId));

            CreateMap<EditEmployeeDto, Employee>()
                .ForMember(x => x.Birthdate, options => options.MapFrom(c => DateTime.Parse(c.Birthdate)))
                .ForMember(x => x.EmployeeTypeId, options => options.MapFrom(c => c.TypeId));
        }
    }
}
