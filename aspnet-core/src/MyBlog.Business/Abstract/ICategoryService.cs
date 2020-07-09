using MyBlog.Core.Entities.Dtos;
using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface ICategoryService
    {

        IDataResult<List<Page<CategoryForReturnDto>>> GetCategories(int startPageNumber, int endPageNumber, int pageSize);
        IDataResult<CategoryForReturnDto> GetCategoryByIdAndLanguage(int id, string languageCode);
        IDataResult<List<CategoryForReturnDto>> GetCategoriesByLanguage(string languageCode);
        IDataResult<CategoryForReturnDto> GetCategoryById(int id);
        IResult InsertCategory(CategoryForCreationDto categoryForCreationDto);
        IResult UpdateCategory(CategoryForUpdateDto categoryForUpdateDto);
        IResult DeleteCategory(CategoryDto categoryDto);
    }
}
