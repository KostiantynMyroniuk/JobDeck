using AutoMapper;
using JobApplication.API.Features.Commands;
using JobApplication.API.Models;

namespace JobApplication.API.Features
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile() 
        {
            CreateMap<Application, ApplicationDto>();
        }
    }
}
