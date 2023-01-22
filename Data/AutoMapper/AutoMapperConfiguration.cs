using AutoMapper;
using Data.Models;
using Data.ViewModels;

namespace Data.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<RegisterRequestVM, User>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<User, UserProfileVM>();
            CreateMap<LoginRequestVM, User>();
        }
    }
}
