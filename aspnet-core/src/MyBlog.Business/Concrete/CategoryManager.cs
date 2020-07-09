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

        public IDataResult<List<Page<CategoryForReturnDto>>> GetCategories(int startPageNumber, int endPageNumber, int pageSize)
        {
            var categories = _categoryTranslationRepository.GetAllIncluding(x => x.Category.IsActive,
                                                                            x => x.Category)
                                                           .OrderByDescending(x => x.Category.CreatedDate)
                                                           .ProjectTo<CategoryForReturnDto>(_mapper.ConfigurationProvider);

            var categoryForReturnDtoPages = Page<CategoryForReturnDto>.CreatePaginatedResultList(categories, startPageNumber, endPageNumber, pageSize);

            return new SuccessDataResult<List<Page<CategoryForReturnDto>>>(Messages.SuccessOperation, categoryForReturnDtoPages);
        }

        public IDataResult<CategoryForReturnDto> GetCategoryByIdAndLanguage(int id, string languageCode)
        {
            var category = _categoryTranslationRepository.GetIncluding(x => x.Language.LanguageCode == languageCode &&
                                                                            x.CategoryId == id,
                                                                       x => x.Category);

            return new SuccessDataResult<CategoryForReturnDto>(Messages.SuccessOperation, _mapper.Map<CategoryForReturnDto>(category));
        }

        public IDataResult<CategoryForReturnDto> GetCategoryById(int id)
        {
            var category = _categoryTranslationRepository.GetIncluding(x => x.CategoryId == id,
                                                                       x => x.Category);

            return new SuccessDataResult<CategoryForReturnDto>(Messages.SuccessOperation, _mapper.Map<CategoryForReturnDto>(category));
        }
        public IDataResult<List<CategoryForReturnDto>> GetCategoriesByLanguage(string languageCode)
        {
            var categories = _categoryTranslationRepository.GetAllIncludingList(x => x.LanguageCode == languageCode,
                                                                                x => x.Category);

            return new SuccessDataResult<List<CategoryForReturnDto>>(Messages.SuccessOperation, _mapper.Map<List<CategoryForReturnDto>>(categories));
        }

        public IResult InsertCategory(CategoryForCreationDto categoryForCreationDto)
        {
            var categoryToBeInserted = _mapper.Map<CategoryTranslation>(categoryForCreationDto);
            _categoryTranslationRepository.Insert(categoryToBeInserted);
            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Category)));
        }

        public IResult UpdateCategory(CategoryForUpdateDto categoryForUpdateDto)
        {
            var categoryToBeUpdated = _mapper.Map<CategoryTranslation>(categoryForUpdateDto);
            _categoryTranslationRepository.Update(categoryToBeUpdated);
            return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Category)));
        }

        public IResult DeleteCategory(CategoryForDeleteDto categoryForDeleteDto)
        {
            //TO DO: Implement transaction

            var categoryToBeDeleted = _mapper.Map<CategoryTranslation>(categoryForDeleteDto);
            _categoryTranslationRepository.Delete(categoryToBeDeleted);
            var category = _categoryRepository.GetIncluding(x => x.Id == categoryForDeleteDto.Id, x => x.CategoryTranslations);

            if (category.CategoryTranslations.Count == 0)
            {
                var categoryToBeSoftDeleted = _mapper.Map<Category>(category);
                _categoryRepository.SoftDelete(categoryToBeSoftDeleted);
            }
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Category)));
        }

    }
}
