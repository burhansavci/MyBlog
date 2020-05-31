using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface IPictureService
    {
        IDataResult<PictureForReturnDto> GetPictureById(int id);
        IDataResult<List<PictureForReturnDto>> GetPicturesByArticleId(int articleId);
        IResult InsertPictureForArticle(PictureForCreationDto pictureDto);
        IResult InsertPicturesForArticle(List<PictureForCreationDto> pictureForCreationDtos);
        IResult DeletePicture(PictureForDeleteDto pictureForDeleteDto);
    }
}
