using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface IPictureService
    {
        IDataResult<PictureForReturnDto> GetPictureById(int id);
        IDataResult<List<PictureForReturnDto>> GetPicturesByArticleId(int articleId);
        IResult InsertPictureForArticle(PictureForCreationDto pictureDto);
        IDataResult<List<PictureForReturnDto>> InsertPicturesForArticle(List<PictureForCreationDto> pictureForCreationDtos, bool skipMainPicture = false);
        IResult DeletePicture(PictureForDeleteDto pictureForDeleteDto);
    }
}
