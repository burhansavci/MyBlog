using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<CategoryDto> GetCategoryById(int id, string languageCode);
        IDataResult<List<CategoryDto>> GetCategories(string languageCode);
        IResult InsertCategory(CategoryDto categoryDto);
        IResult UpdateCategory(CategoryDto categoryDto);
        IResult DeleteCategory(CategoryDto categoryDto);
    }
}
