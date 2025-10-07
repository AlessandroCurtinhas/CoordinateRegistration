using AutoMapper;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class MarkerMap : Profile
    {
        public MarkerMap() {

            CreateMap<Marker, MarkerDto>()
              .ForMember(e => e.TypeOcurrences, opts => opts
              .MapFrom(e => e.MarkerTypeOccurrences.Select(e => new MarkerDtoTypeOccurrence { Hash = e.TypeOccurrence.Hash, Name =  e.TypeOccurrence.Name })))          
              .ForMember(dest => dest.UserName, opts => opts
              .MapFrom(src => src.User.Name))
              .ForMember(dest => dest.PositiveTotal, opts => opts
              .MapFrom(src => src.Reviews.Count(x => x.Positive == true)))
              .ForMember(dest => dest.PositivePercentual, opts => opts
              .MapFrom(src => src.Reviews.Count() == 0 ? 0 : (((double)src.Reviews.Count(x => x.Positive == true)/(double)src.Reviews.Count()) * 100)));

            CreateMap<MarkerAddDto, Marker>()
                .ForMember(dest => dest.Hash, opt => opt
                .MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<MarkerPutDto, Marker>()
                .ForMember(dest => dest.DateUpdated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

        }

    }
}
