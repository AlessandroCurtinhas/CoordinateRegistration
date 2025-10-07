using AutoMapper;
using CoordinateRegistration.Application.Dto.Review;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class ReviewMap : Profile
    {
        public ReviewMap()
        {
            CreateMap<ReviewAddDto, Review>()
                .ForMember(dest => dest.Hash, opt => opt
                .MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<ReviewPutDto, Review>()
               .ForMember(dest => dest.DateUpdated, opt => opt
               .MapFrom(src => DateTime.UtcNow));

            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt
                .MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MarkerHash, opt => opt
                .MapFrom(src => src.Marker.Hash));

        }
    }
}
