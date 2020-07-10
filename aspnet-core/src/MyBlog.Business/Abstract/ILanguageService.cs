using MyBlog.Core.Entities.Dtos;
using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface ILanguageService
    {
        IDataResult<LanguageDto> GetLanguageByCode(string languageCode);
        IDataResult<List<LanguageDto>> GetLanguages();
        IDataResult<List<Page<LanguageDto>>> GetPaginatedLanguages(int startPageNumber, int endPageNumber, int pageSize);
        IResult InsertLanguage(LanguageDto languageDto);
        IResult UpdateLanguage(LanguageDto languageDto);
        IResult DeleteLanguage(LanguageDto languageDto);
    }
}
