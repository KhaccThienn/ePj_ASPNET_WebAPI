using ASPNET_WebAPI.Models.Domains;
using ASPNET_WebAPI.Models.DTOs;
using AutoMapper;

namespace ASPNET_WebAPI.Models.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeImage>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeImage>().ReverseMap();
        }
    }
}
