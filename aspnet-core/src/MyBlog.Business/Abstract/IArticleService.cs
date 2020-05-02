using MyBlog.Core.Entities.Dtos;
using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface IArticleService
    {
        IDataResult<ArticleForReturnDto> GetArticleById(string languageCode, int id);
        IDataResult<Page<ArticleForReturnDto>> GetArticlesByCategoryId(string languageCode, int categoryId, int pageNumber, int pageSize);
        IDataResult<Page<ArticleForReturnDto>> GetArticles(string languageCode, int pageNumber, int pageSize);
        IResult InsertArticle(ArticleDto articleDto);
        IResult UpdateArticle(ArticleDto articleDto);
        IResult DeleteArticle(ArticleDto articleDto);
    }
}
