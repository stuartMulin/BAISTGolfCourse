using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Helpers;

namespace TheBackEndLayer.Infrastructure.Maps
{
    public class EmpViewModel: IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<DbModels.Employees, ViewModels.Employee.EmployeeViewModel>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address1));
        }
    }
}
