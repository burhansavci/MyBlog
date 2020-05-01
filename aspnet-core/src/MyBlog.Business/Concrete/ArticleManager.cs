using AutoMapper;
using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Entities.Dtos;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private IArticleTranslationRepository _articleTranslationRepository;
        private IArticleRepository _articleRepository;
        private IMapper _mapper;
        public ArticleManager(IArticleTranslationRepository articleTranslationRepository, IArticleRepository articleRepository, IMapper mapper)
        {
            _articleTranslationRepository = articleTranslationRepository;
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public IDataResult<List<ArticleForReturnDto>> GetArticles(string languageCode, int pageNumber, int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Language.LanguageCode == languageCode && x.Article.IsActive,                                                              x => x.Article.Category.CategoryTranslations,
                                                                         x => x.Article.Pictures);

            var pagedList = PagedList<ArticleTranslation>.Create(articles, pageNumber, pageSize);

            return new SuccessDataResult<List<ArticleForReturnDto>>(Messages.SuccessOperation, 
                                                                    _mapper.Map<List<ArticleForReturnDto>>(pagedList));
        }

        public IDataResult<List<ArticleForReturnDto>> GetArticlesByCategoryId(string languageCode, int categoryId, int pageNumber, int pageSize)
        {
            var articles = _articleTranslationRepository.GetAllIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                              x.Article.CategoryId == categoryId &&
                                                                              x.Article.IsActive,
                                                                         x => x.Article.Category.CategoryTranslations,
                                                                         x => x.Article.Pictures);

            var pagedList = PagedList<ArticleTranslation>.Create(articles, pageNumber, pageSize);

            return new SuccessDataResult<List<ArticleForReturnDto>>(Messages.SuccessOperation,
                                                                    _mapper.Map<List<ArticleForReturnDto>>(pagedList));
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

        public IResult InsertArticle(ArticleDto articleDto)
        {
            var articleToBeInserted = _mapper.Map<ArticleTranslation>(articleDto);
            _articleTranslationRepository.Insert(articleToBeInserted);
            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Article)));
        }

        public IResult UpdateArticle(ArticleDto articleDto)
        {
            if (articleDto.ArticleId == null)
            {
                var articleToBeUpdated = _mapper.Map<Article>(articleDto);
                _articleRepository.Update(articleToBeUpdated);
                return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Article)));
            }
            var articleWithTranslationToBeUpdated = _mapper.Map<ArticleTranslation>(articleDto);
            _articleTranslationRepository.Update(articleWithTranslationToBeUpdated);
            return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Article)));
        }
        public IResult DeleteArticle(ArticleDto articleDto)
        {
            if (articleDto.ArticleId != null)
            {
                var articleToBeSoftDeleted = _mapper.Map<Article>(articleDto);
                _articleRepository.SoftDelete(articleToBeSoftDeleted);
            }
            var articleToBeDeleted = _mapper.Map<ArticleTranslation>(articleDto);
            _articleTranslationRepository.Delete(articleToBeDeleted);
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Article)));
        }
    }
}
