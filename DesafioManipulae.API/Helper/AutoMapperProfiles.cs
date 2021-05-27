using AutoMapper;
using DesafioManipulae.API.Dtos;
using DesafioManipulae.Domain.Indentity;

namespace DesafioManipulae.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}