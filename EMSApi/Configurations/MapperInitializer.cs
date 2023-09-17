using AutoMapper;
using EMSApi.Data;
using EMSApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();

            CreateMap<APIUser, UserDTO>().ReverseMap();
            CreateMap<APIUser, LoginUserDTO>().ReverseMap();
        }
    }
}
