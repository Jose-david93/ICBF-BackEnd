using AutoMapper;
using ICBF.Domain.DTO;
using ICBF.Domain.Models;

namespace ICBF.Domain.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
