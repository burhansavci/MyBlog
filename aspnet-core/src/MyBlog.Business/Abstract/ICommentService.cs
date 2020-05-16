using MyBlog.Core.Utilities.Results;
using MyBlog.Entities.Dtos;
using System.Collections.Generic;

namespace MyBlog.Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<CommentForReturnDto>> GetCommentsByArticleId(int articleId);
        IDataResult<List<CommentForReturnDto>> GetComments();
        IResult InsertComment(CommentDto commentDto);
        IResult UpdateComment(CommentDto commentDto);
        IResult DeleteComment(CommentDto commentDto);
    }
}
