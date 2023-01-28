using AutoMapper;
using Data.Models;
using Data.ViewModels;
using Google.Apis.Auth;

namespace Data.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            // user
            CreateMap<RegisterRequestVM, User>()
                .ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<User, UserProfileVM>();
            CreateMap<LoginRequestVM, User>();
            CreateMap<GoogleJsonWebSignature.Payload, User>()
                .ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email))
                .ForMember(dst => dst.Name, act => act.MapFrom(src => src.GivenName))
                .ForMember(dst => dst.Surname, act => act.MapFrom(src => src.FamilyName))
                .ForMember(dst => dst.Photo, act => act.MapFrom(src => src.Picture))
                .ForMember(dst => dst.EmailConfirmed, act => act.MapFrom(src => src.EmailVerified));

            CreateMap<User, FrontUserVM>();
            // Category

            CreateMap<CreateCategoryRequestVM, Category>();

            // Ticket

            CreateMap<CreateTicketRequestVM, Ticket>();
            CreateMap<Ticket, FrontTicketVM>();
            CreateMap<FrontTicketVM, Ticket>();

            // Photos
            CreateMap<TicketPhoto, FrontTicketPhotoVM>();
            CreateMap<FrontTicketPhotoVM, TicketPhoto>();
        }
    }
}
