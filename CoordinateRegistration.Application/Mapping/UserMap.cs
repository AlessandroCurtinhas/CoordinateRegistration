using AutoMapper;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Dto.User;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class UserMap : Profile
    {
        public UserMap() 
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserAuthenticatedDto>();

            CreateMap<UserAddDto, User>()
                .ForMember(dest => dest.Hash, opt => opt
                .MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<UserPutDto, User>()
                .ForMember(dest => dest.DateUpdated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<UserRecoveryPasswordDto, User>()
                .ForMember(dest => dest.DateUpdated, opt => opt
                .MapFrom(src => DateTime.UtcNow));


        }
    }
}
