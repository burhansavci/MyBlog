using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class ArticleMapperProfile : Profile
    {
        public ArticleMapperProfile()
        {
            CreateMap<ArticleTranslation, ArticleDto>()
                .IncludeMembers(s => s.Article);

            CreateMap<Article, ArticleDto>(MemberList.None);

            CreateMap<ArticleDto, Article>();

            CreateMap<ArticleDto, ArticleTranslation>()
                .ForPath(d => d.Article.PublishDate, opt => opt.MapFrom(s => s.PublishDate))
                .ForPath(d => d.Article.Id, opt => opt.MapFrom(s => s.ArticleId))
                .ForPath(d => d.Article.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(d => d.Article.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                .AfterMap((s, d) =>
                {
                    if (s.ArticleId != null)
                        d.Article = null;
                });
        }
    }
}
