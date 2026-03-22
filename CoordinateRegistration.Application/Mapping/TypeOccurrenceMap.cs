using AutoMapper;
using CoordinateRegistration.Application.Dto.TypeOccurrence;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class TypeOccurrenceMap : Profile
    {
        public TypeOccurrenceMap() {

            CreateMap<TypeOccurrence, TypeOccurrenceDto>()
                .ForMember(dest => dest.PersonNameCreated, opt => opt
                .MapFrom(src => src.PersonCreate.Name))
                .ForMember(dest => dest.PersonNameUpdated, opt => opt
                .MapFrom(src => src.PersonUpdate.Name))
                .ForMember(dest => dest.PersonNameDeleted, opt => opt
                .MapFrom(src => src.PersonDelete.Name));

            CreateMap<TypeOccurrence, TypeOccurrenceDtoPerson>()
                .ForMember(dest => dest.PersonNameCreated, opt => opt
                .MapFrom(src => src.PersonCreate.Name))
                .ForMember(dest => dest.PersonNameUpdated, opt => opt
                .MapFrom(src => src.PersonUpdate.Name));


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
