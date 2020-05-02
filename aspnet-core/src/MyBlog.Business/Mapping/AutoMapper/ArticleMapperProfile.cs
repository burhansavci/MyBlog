using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System.Linq;

namespace MyBlog.Business.Mapping.AutoMapper
{
    public class ArticleMapperProfile : Profile
    {
        public ArticleMapperProfile()
        {
            CreateMap<ArticleTranslation, ArticleForReturnDto>()
                .ForMember(d => d.Picture, opt => opt.MapFrom(s => s.Article.Pictures.FirstOrDefault(p => p.IsMain)))
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Article.Category.CategoryTranslations
                                                                     .FirstOrDefault(ct => ct.LanguageCode == s.LanguageCode)))
                .ForMember(d => d.ViewCount, opt => opt.MapFrom(s => s.Article.ViewCount))
                .ForMember(d => d.PublishDate, opt => opt.MapFrom(s => s.Article.PublishDate));


            CreateMap<ArticleDto, Article>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId != null ? s.Id : s.ArticleId));

            CreateMap<ArticleDto, ArticleTranslation>()
                .ForPath(d => d.Article.PublishDate, opt => opt.MapFrom(s => s.PublishDate))
                .ForPath(d => d.Article.Id, opt => opt.MapFrom(s => s.ArticleId))
                .ForPath(d => d.Article.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(d => d.Article.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                .AfterMap((s, d) =>
                {
                    if (s.ArticleId != null || s.UserId == null)
                        d.Article = null;
                });
        }
    }
}
