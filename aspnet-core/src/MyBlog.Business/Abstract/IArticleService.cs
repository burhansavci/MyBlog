using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface IArticleService
    {
        IDataResult<ArticleDto> GetArticleById(int id, string languageCode);
        IDataResult<ArticleDto> GetArticlesByCategoryId(int categoryId, string languageCode);
        IDataResult<List<ArticleDto>> GetArticles(string languageCode);
        IResult InsertArticle(ArticleDto articleDto);
        IResult UpdateArticle(ArticleDto articleDto);
        IResult DeleteArticle(ArticleDto articleDto);
    }
}
