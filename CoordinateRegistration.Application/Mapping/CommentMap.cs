using AutoMapper;
using CoordinateRegistration.Application.Dto.Comment;
using CoordinateRegistration.Domain;

namespace CoordinateRegistration.Application.Mapping
{
    public class CommentMap : Profile
    {
        public CommentMap() 
        {

            CreateMap<CommentAddDto, Comment>()
                .ForMember(dest => dest.Hash, opt => opt
                .MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.DateCreated, opt => opt
                .MapFrom(src => DateTime.UtcNow));

            CreateMap<CommentPutDto, Comment>()
               .ForMember(dest => dest.DateUpdated, opt => opt
               .MapFrom(src => DateTime.UtcNow));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt
                .MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MarkerHash, opt => opt
                .MapFrom(src => src.Marker.Hash))
                .ForMember(dest => dest.CommentHash, opt => opt
                .MapFrom(src => src.Hash));
        }
    }
}
