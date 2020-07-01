using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Entities.Dtos;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyBlog.Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleTranslationRepository _articleTranslationRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IPictureService _pictureService;
        private readonly IMapper _mapper;

        public ArticleManager(IArticleTranslationRepository articleTranslationRepository,
                              IArticleRepository articleRepository,
                              IPictureService pictureService,
                              IMapper mapper)
        {
            _articleTranslationRepository = articleTranslationRepository;
            _articleRepository = articleRepository;
            _pictureService = pictureService;
            _mapper = mapper;
        }

        public IDataResult<List<Page<ArticleForReturnDto>>> GetArticles(int startPageNumber, int endPageNumber, int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Article.IsActive,
                                                                         x => x.Article.Category.CategoryTranslations,
                                                                         x => x.Article.Pictures)
                                                        .OrderByDescending(x => x.Article.PublishDate)
                                                        .ProjectTo<ArticleForReturnDto>(_mapper.ConfigurationProvider);

            var articleForReturnDtoPages = Page<ArticleForReturnDto>.CreatePaginatedResultList(articles, startPageNumber, endPageNumber, pageSize);

            return new SuccessDataResult<List<Page<ArticleForReturnDto>>>(Messages.SuccessOperation, articleForReturnDtoPages);
        }

        public IDataResult<Page<ArticleForReturnDto>> GetArticlesByLanguageCode(string languageCode, int pageNumber, int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                              x.Article.IsActive,
                                                                         x => x.Article.Category.CategoryTranslations,
                                                                         x => x.Article.Pictures)
                                                        .OrderByDescending(x => x.Article.PublishDate);

            var articleTranslationPage = Page<ArticleTranslation>.CreatePaginatedResult(articles, pageNumber, pageSize);
            var articleForReturnDtos = _mapper.Map<List<ArticleForReturnDto>>(articleTranslationPage.Items);
            var articleForReturnDtoPage = new Page<ArticleForReturnDto>(articleForReturnDtos,
                                                                        articleTranslationPage.TotalCount,
                                                                        articleTranslationPage.CurrentPage,
                                                                        articleTranslationPage.PageSize);

            return new SuccessDataResult<Page<ArticleForReturnDto>>(Messages.SuccessOperation, articleForReturnDtoPage);
        }

        public IDataResult<Page<ArticleForReturnDto>> GetArticlesByCategoryId(string languageCode,
                                                                              int categoryId,
                                                                              int pageNumber,
                                                                              int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                              x.Article.CategoryId == categoryId &&
                                                                              x.Article.IsActive,
                                                                         x => x.Article.Category.CategoryTranslations,
                                                                         x => x.Article.Pictures);

            var articleTranslationPage = Page<ArticleTranslation>.CreatePaginatedResult(articles, pageNumber, pageSize);
            var articleForReturnDtos = _mapper.Map<List<ArticleForReturnDto>>(articleTranslationPage.Items);
            var articleForReturnDtoPage = new Page<ArticleForReturnDto>(articleForReturnDtos,
                                                                        articleTranslationPage.TotalCount,
                                                                        articleTranslationPage.CurrentPage,
                                                                        articleTranslationPage.PageSize);

            return new SuccessDataResult<Page<ArticleForReturnDto>>(Messages.SuccessOperation, articleForReturnDtoPage);
        }

        public IDataResult<ArticleForReturnDto> GetArticleById(string languageCode, int id)
        {
            var article = _articleTranslationRepository.GetIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                          x.ArticleId == id &&
                                                                          x.Article.IsActive,
                                                                     x => x.Article.Category.CategoryTranslations,
                                                                     x => x.Article.Pictures);

            return new SuccessDataResult<ArticleForReturnDto>(Messages.SuccessOperation, _mapper.Map<ArticleForReturnDto>(article));
        }

        public IDataResult<List<ArticleForArchiveReturnDto>> GetArticlesArchive(string languageCode)
        {
            var articles = _articleTranslationRepository.GetAllIncludingList(x => x.LanguageCode == languageCode, x => x.Article)
                                                        .OrderByDescending(x => x.Article.PublishDate)
                                                        .GroupBy(x => new ArticleForArchiveReturnDto
                                                        {
                                                            PublishYear = x.Article.PublishDate.Year
                                                        })
                                                        .AsQueryable()
                                                        .ProjectTo<ArticleForArchiveReturnDto>(_mapper.ConfigurationProvider)
                                                        .ToList();


            return new SuccessDataResult<List<ArticleForArchiveReturnDto>>(Messages.SuccessOperation, articles);
        }

        public IDataResult<Page<ArticleForReturnDto>> GetArticlesByYear(string languageCode,
                                                                        int year,
                                                                        int pageNumber,
                                                                        int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                              x.Article.IsActive &&
                                                                              x.Article.PublishDate.Year == year,
                                                                         x => x.Article.Category.CategoryTranslations,
                                                                         x => x.Article.Pictures)
                                                        .OrderByDescending(x => x.Article.PublishDate);

            var articleTranslationPage = Page<ArticleTranslation>.CreatePaginatedResult(articles, pageNumber, pageSize);
            var articleForReturnDtos = _mapper.Map<List<ArticleForReturnDto>>(articleTranslationPage.Items);
            var articleForReturnDtoPage = new Page<ArticleForReturnDto>(articleForReturnDtos,
                                                                        articleTranslationPage.TotalCount,
                                                                        articleTranslationPage.CurrentPage,
                                                                        articleTranslationPage.PageSize);

            return new SuccessDataResult<Page<ArticleForReturnDto>>(Messages.SuccessOperation, articleForReturnDtoPage);
        }

        public IDataResult<Page<ArticleForReturnDto>> GetArticlesByYearAndMonth(string languageCode,
                                                                                int year,
                                                                                int month,
                                                                                int pageNumber,
                                                                                int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                  x.Article.IsActive &&
                                                                  x.Article.PublishDate.Year == year &&
                                                                  x.Article.PublishDate.Month == month,
                                                             x => x.Article.Category.CategoryTranslations,
                                                             x => x.Article.Pictures)
                                            .OrderByDescending(x => x.Article.PublishDate);

            var articleTranslationPage = Page<ArticleTranslation>.CreatePaginatedResult(articles, pageNumber, pageSize);
            var articleForReturnDtos = _mapper.Map<List<ArticleForReturnDto>>(articleTranslationPage.Items);
            var articleForReturnDtoPage = new Page<ArticleForReturnDto>(articleForReturnDtos,
                                                                        articleTranslationPage.TotalCount,
                                                                        articleTranslationPage.CurrentPage,
                                                                        articleTranslationPage.PageSize);

            return new SuccessDataResult<Page<ArticleForReturnDto>>(Messages.SuccessOperation, articleForReturnDtoPage);
        }

        public IResult InsertArticle(ArticleForCreationDto articleForCreationDto)
        {
            //TO DO: Implement transaction

            var articleToBeInserted = _mapper.Map<ArticleTranslation>(articleForCreationDto);
            var insertedArticle = _articleTranslationRepository.Insert(articleToBeInserted);

            articleForCreationDto.Pictures.ForEach(p => p.ArticleId = insertedArticle.ArticleId);

            var result = _pictureService.InsertPicturesForArticle(articleForCreationDto.Pictures);

            if (result.Success)
            {
                var imgRegex = new Regex("<img src=\"(?<url>(data:(?<type>.+?);base64),(?<data>[^\"]+))\"");
                foreach (var picture in result.Data.Where(x => !x.IsMain))
                {
                    insertedArticle.ContentMain = imgRegex.Replace(insertedArticle.ContentMain,
                                                                    m => $"<img src=\"{picture.Url}\"", 1);
                }

                _articleTranslationRepository.Update(insertedArticle);

                return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Article)));
            }
            else
                return new ErrorResult($"Picture couldn't be inserted Error Message {result.Message}");

        }

        public IResult UpdateArticle(ArticleForUpdateDto articleForUpdateDto)
        {
            //TO DO: Implement transaction

            Article article = _articleRepository.GetIncluding(x => x.Id == articleForUpdateDto.Id,
                                              x => x.Pictures);
            var picturesDict = article.Pictures.ToDictionary(x => x.PublicId);

            var toBeDeletedPictures = _mapper.Map<List<PictureForDeleteDto>>(picturesDict.Where(x => !articleForUpdateDto.Pictures
                                                                                                                         .Select(y => y.PublicId)
                                                                                                                         .Contains(x.Key))
                                                                                         .Select(x => x.Value));
            foreach (var toBeDeletedPicture in toBeDeletedPictures)
            {
                _pictureService.DeletePicture(toBeDeletedPicture);
            }

            var picturesToBeCreated = articleForUpdateDto.Pictures.Where(x => x.PublicId == null).Select(x => new PictureForCreationDto
            {
                ArticleId = article.Id,
                IsMain = x.IsMain,
                File = x.File
            }).ToList();

            var articleWithTranslationToBeUpdated = _mapper.Map<ArticleTranslation>(articleForUpdateDto);
            if (picturesToBeCreated.Count > 0)
            {
                bool skipMainPicture = picturesToBeCreated.Any(x => x.IsMain) ? false : true;
                var result = _pictureService.InsertPicturesForArticle(picturesToBeCreated, skipMainPicture);

                if (result.Success)
                {
                    var imgRegex = new Regex("<img src=\"(?<url>(data:(?<type>.+?);base64),(?<data>[^\"]+))\"");
                    foreach (var picture in result.Data.Where(x => !x.IsMain))
                    {
                        articleWithTranslationToBeUpdated.ContentMain = imgRegex.Replace(articleWithTranslationToBeUpdated.ContentMain,
                                                                        m => $"<img src=\"{picture.Url}\"", 1);
                    }

                    _articleTranslationRepository.Update(articleWithTranslationToBeUpdated);
                    return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Article)));
                }
                else
                    return new ErrorResult($"Picture couldn't be inserted Error Message {result.Message}");
            }
            _articleTranslationRepository.Update(articleWithTranslationToBeUpdated);
            return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Article)));
        }
        public IResult DeleteArticle(ArticleForDeleteDto articleForDeleteDto)
        {
            //TO DO: Implement transaction

            var articleToBeDeleted = _mapper.Map<ArticleTranslation>(articleForDeleteDto);
            _articleTranslationRepository.Delete(articleToBeDeleted);
            var article = _articleRepository.GetIncluding(x => x.Id == articleForDeleteDto.Id, x => x.ArticleTranslations);

            if (article.ArticleTranslations.Count == 0)
            {
                var articleToBeSoftDeleted = _mapper.Map<Article>(article);
                _articleRepository.SoftDelete(articleToBeSoftDeleted);
            }
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Article)));
        }
    }
}
