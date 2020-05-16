using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<CommentDto, Comment>();

            CreateMap<Comment, CommentForReturnDto>()
                .ForMember(s => s.SubComments, opt => opt.MapFrom(d => d.Comments));

        }
    }
}
