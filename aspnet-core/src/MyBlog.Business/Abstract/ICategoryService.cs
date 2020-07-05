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
        IResult InsertCategory(CategoryDto categoryDto);
        IResult UpdateCategory(CategoryDto categoryDto);
        IResult DeleteCategory(CategoryDto categoryDto);
    }
}
