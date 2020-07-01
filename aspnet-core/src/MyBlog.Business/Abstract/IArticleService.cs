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
        IDataResult<List<Page<ArticleForReturnDto>>> GetArticles(int startPageNumber, int endPageNumber, int pageSize);
        IDataResult<Page<ArticleForReturnDto>> GetArticlesByLanguageCode(string languageCode, int pageNumber, int pageSize);
        IDataResult<List<ArticleForArchiveReturnDto>> GetArticlesArchive(string languageCode);
        IDataResult<Page<ArticleForReturnDto>> GetArticlesByYear(string languageCode, int year, int pageNumber, int pageSize);
        IDataResult<Page<ArticleForReturnDto>> GetArticlesByYearAndMonth(string languageCode, int year, int month, int pageNumber, int pageSize);
        IResult InsertArticle(ArticleForCreationDto articleForCreationDto);
        IResult UpdateArticle(ArticleForUpdateDto articleForUpdateDto);
        IResult DeleteArticle(ArticleForDeleteDto articleDto);
    }
}
