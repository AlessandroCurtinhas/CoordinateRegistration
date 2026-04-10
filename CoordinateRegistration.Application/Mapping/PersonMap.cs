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
            CreateMap<Person, PersonAuthenticatedDto>()
                .ForMember(e => e.Cities, opts => opts
                .MapFrom(e => e.Cities.Select(e => new PersonCityDto { Hash = e.Hash, Name = e.Name, State = e.State, UF = e.UF })));

            CreateMap<PersonAddDto, Person>()
                .ForMember(e => e.Cities, opts => opts
                .MapFrom(e => e.Cities.Select(e => new PersonCity { Hash = Guid.NewGuid(), Name = e.Name, State = e.State, UF = e.UF })))
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
