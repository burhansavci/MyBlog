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
                .ForMember(d => d.Pictures, opt => opt.MapFrom(s => s.Article.Pictures.Where(p => !p.IsMain)))
                .ForMember(d => d.MainPicture, opt => opt.MapFrom(s => s.Article.Pictures.FirstOrDefault(p => p.IsMain)))
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Article.Category.CategoryTranslations
                                                                     .FirstOrDefault(ct => ct.LanguageCode == s.LanguageCode)))
                .ForPath(d => d.Category.Id, opt => opt.MapFrom(s => s.Article.CategoryId))
                .ForMember(d => d.ViewCount, opt => opt.MapFrom(s => s.Article.ViewCount))
                .ForMember(d => d.CommentCount, opt => opt.MapFrom(s => s.Article.Comments.Count))
                .ForMember(d => d.PublishDate, opt => opt.MapFrom(s => s.Article.PublishDate))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ArticleId))
                .ForMember(d => d.ArticleTranslationId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Article.UserId));

            CreateMap<ArticleForDeleteDto, ArticleTranslation>()
                .ForMember(d => d.ArticleId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ArticleTranslationId));

            CreateMap<ArticleForUpdateDto, ArticleTranslation>()
                 .ForPath(d => d.Article.PublishDate, opt => opt.MapFrom(s => s.PublishDate))
                 .ForPath(d => d.Article.Id, opt => opt.MapFrom(s => s.Id))
                 .ForPath(d => d.Article.UserId, opt => opt.MapFrom(s => s.UserId))
                 .ForPath(d => d.Article.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                 .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ArticleTranslationId));

            CreateMap<ArticleForCreationDto, ArticleTranslation>()
                .ForPath(d => d.Article.PublishDate, opt => opt.MapFrom(s => s.PublishDate))
                .ForPath(d => d.Article.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(d => d.Article.CategoryId, opt => opt.MapFrom(s => s.CategoryId));

            CreateMap<IGrouping<ArticleForArchiveMonthDto, ArticleTranslation>, ArticleForArchiveMonthDto>()
                .ForMember(d => d.MonthName, opt => opt.MapFrom(s => s.FirstOrDefault().Article.PublishDate.ToString("MMMM")))
                .ForMember(d => d.PublishMonth, opt => opt.MapFrom(s => s.Key.PublishMonth))
                .ForMember(d => d.CountByMonth, opt => opt.MapFrom(s => s.Count()))
                .ForMember(d => d.ArticleArchives, opt => opt.MapFrom(s => s.Select(x =>
                                                          new ArticleForArchiveDto
                                                          {
                                                              Id = x.ArticleId,
                                                              Title = x.Title
                                                          })));
            CreateMap<IGrouping<ArticleForArchiveReturnDto, ArticleTranslation>, ArticleForArchiveReturnDto>()
                .ForMember(d => d.CountByYear, opt => opt.MapFrom(s => s.Count()))
                .ForMember(d => d.PublishYear, opt => opt.MapFrom(s => s.Key.PublishYear))
                .ForMember(d => d.Months, opt => opt.MapFrom(s => s.GroupBy(x =>
                                                                  new ArticleForArchiveMonthDto
                                                                  {
                                                                      PublishMonth = x.Article.PublishDate.Month
                                                                  })));

        }
    }
}
