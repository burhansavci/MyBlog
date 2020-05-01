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
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryManager(ICategoryTranslationRepository categoryTranslationRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryTranslationRepository = categoryTranslationRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IDataResult<CategoryForReturnDto> GetCategoryByIdAndLanguage(int id, string languageCode)
        {
            var category = _categoryTranslationRepository.GetIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                            x.CategoryId == id, 
                                                                       x => x.Category);

            return new SuccessDataResult<CategoryForReturnDto>(Messages.SuccessOperation, _mapper.Map<CategoryForReturnDto>(category));
        }

        public IDataResult<List<CategoryForReturnDto>> GetCategoriesByLanguage(string languageCode)
        {
            var categories = _categoryTranslationRepository.GetAllIncludingList(x => x.LanguageCode == languageCode, 
                                                                                x => x.Category);

            return new SuccessDataResult<List<CategoryForReturnDto>>(Messages.SuccessOperation, _mapper.Map<List<CategoryForReturnDto>>(categories));
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
            if (categoryDto.CategoryId != null)
            {
                var categoryToBeSoftDeleted = _mapper.Map<Category>(categoryDto);
                _categoryRepository.SoftDelete(categoryToBeSoftDeleted);
            }
            var categoryToBeDeleted = _mapper.Map<CategoryTranslation>(categoryDto);
            _categoryTranslationRepository.Delete(categoryToBeDeleted);
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Category)));
        }

    }
}
