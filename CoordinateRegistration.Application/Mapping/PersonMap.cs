using AutoMapper;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Dto.Person;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class PersonMap : Profile
    {
        public PersonMap() 
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, PersonAuthenticatedDto>();

            CreateMap<PersonAddDto, Person>()
                .ForMember(dest => dest.Hash, opt => opt
                .MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<PersonPutDto, Person>()
                .ForMember(dest => dest.DateUpdated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<PersonRecoveryPasswordDto, Person>()
                .ForMember(dest => dest.DateUpdated, opt => opt
                .MapFrom(src => DateTime.UtcNow));


        }
    }
}
