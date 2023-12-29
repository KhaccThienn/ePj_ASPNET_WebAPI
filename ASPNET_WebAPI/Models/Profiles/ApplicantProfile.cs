using ASPNET_WebAPI.Models.Domains;
using ASPNET_WebAPI.Models.DTOs;
using AutoMapper;

namespace ASPNET_WebAPI.Models.Profiles
{
    public class ApplicantProfile : Profile
    {
        public ApplicantProfile()
        {
            CreateMap<Applicant, ApplicantImage>().ReverseMap();
            CreateMap<Applicant, UpdateApplicantImage>().ReverseMap();
        }
    }
}
