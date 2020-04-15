using AutoMapper;
using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IDataResult<List<ArticleDto>> GetArticles(string languageCode)
        {
            var articles = _articleTranslationRepository.GetAllIncludingList(x => x.Language.LanguageCode == languageCode &&
                                                                                  x.Article.IsActive, x => x.Article);
            return new SuccessDataResult<List<ArticleDto>>(Messages.SuccessOperation, _mapper.Map<List<ArticleDto>>(articles));
        }

        public IDataResult<ArticleDto> GetArticlesByCategoryId(int categoryId, string languageCode)
        {
            var article = _articleTranslationRepository.GetIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                          x.Article.CategoryId == categoryId &&
                                                                          x.Article.IsActive, x => x.Article);
            return new SuccessDataResult<ArticleDto>(Messages.SuccessOperation, _mapper.Map<ArticleDto>(article));
        }

        public IDataResult<ArticleDto> GetArticleById(int id, string languageCode)
        {
            var article = _articleTranslationRepository.GetIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                          x.ArticleId == id &&
                                                                          x.Article.IsActive, x => x.Article);
            return new SuccessDataResult<ArticleDto>(Messages.SuccessOperation, _mapper.Map<ArticleDto>(article));
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
           //var articleToBeDeleted = _mapper.Map<ArticleTranslation>(articleDto);
            //_articleTranslationRepository.Delete(articleToBeDeleted);
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Article)));
        }
    }
}
