using AutoMapper;
using MyBlog.Business.Abstract;
using MyBlog.Business.Constants;
using MyBlog.Core.Utilities.Results;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryTranslationRepository _categoryTranslationRepository;
        private IMapper _mapper;
        public CategoryManager(ICategoryTranslationRepository categoryTranslationRepository, IMapper mapper)
        {
            _categoryTranslationRepository = categoryTranslationRepository;
            _mapper = mapper;
        }

        public IDataResult<CategoryDto> GetCategoryById(int id, string languageCode)
        {
            var category = _categoryTranslationRepository.Get(x => x.Language.LanguageCode == languageCode && x.CategoryId == id);
            return new SuccessDataResult<CategoryDto>(Messages.SuccessOperation, _mapper.Map<CategoryDto>(category));
        }

        public IDataResult<List<CategoryDto>> GetCategories(string languageCode)
        {
            var categories = _categoryTranslationRepository.GetAllList(x => x.Language.LanguageCode == languageCode);
            return new SuccessDataResult<List<CategoryDto>>(Messages.SuccessOperation, _mapper.Map<List<CategoryDto>>(categories));
        }

        public IResult InsertCategory(CategoryDto categoryDto)
        {
            var categoryToBeInserted = _mapper.Map<CategoryTranslation>(categoryDto);
            _categoryTranslationRepository.Insert(categoryToBeInserted);
            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Category)));
        }

        public IResult UpdateCategory(CategoryDto categoryDto)
        {
            var categoryToBeUpdated = _mapper.Map<CategoryTranslation>(categoryDto);
            _categoryTranslationRepository.Update(categoryToBeUpdated);
            return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Category)));
        }

        public IResult DeleteCategory(CategoryDto categoryDto)
        {
            var categoryToBeDeleted = _mapper.Map<CategoryTranslation>(categoryDto);
            _categoryTranslationRepository.Delete(categoryToBeDeleted);
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Category)));
        }

    }
}
