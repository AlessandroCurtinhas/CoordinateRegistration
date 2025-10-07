using AutoMapper;
using CoordinateRegistration.Application.Dto.TypeOccurrence;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class TypeOccurrenceMap : Profile
    {
        public TypeOccurrenceMap() {

            CreateMap<TypeOccurrence, TypeOccurrenceDto>()
                .ForMember(dest => dest.UserNameCreated, opt => opt
                .MapFrom(src => src.UserCreate.Name))
                .ForMember(dest => dest.UserNameUpdated, opt => opt
                .MapFrom(src => src.UserUpdate.Name))
                .ForMember(dest => dest.UserNameDeleted, opt => opt
                .MapFrom(src => src.UserDelete.Name));

            CreateMap<TypeOccurrence, TypeOccurrenceDtoUser>()
                .ForMember(dest => dest.UserNameCreated, opt => opt
                .MapFrom(src => src.UserCreate.Name))
                .ForMember(dest => dest.UserNameUpdated, opt => opt
                .MapFrom(src => src.UserUpdate.Name));


            CreateMap<TypeOccurrencePutDto, TypeOccurrence>()
                .ForMember(dest => dest.DateUpdated, opt => opt
                .MapFrom(src => DateTime.UtcNow));


            CreateMap<TypeOccurrenceAddDto, TypeOccurrence>()
                .ForMember(dest => dest.Hash, opt => opt
                .MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

        }
    }
}
