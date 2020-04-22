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
    public class CommentManager : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;

        public CommentManager(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public IDataResult<List<CommentDto>> GetCommentsByArticleId(int articleId)
        {
            var comment = _commentRepository.GetAllIncludingList(x => x.ArticleId == articleId, x => x.Parent);
            return new SuccessDataResult<List<CommentDto>>(Messages.SuccessOperation, _mapper.Map<List<CommentDto>>(comment));
        }

        public IDataResult<List<CommentDto>> GetComments()
        {
            var comment = _commentRepository.GetAllIncludingList(x => x.Parent);
            return new SuccessDataResult<List<CommentDto>>(Messages.SuccessOperation, _mapper.Map<List<CommentDto>>(comment));
        }

        public IResult InsertComment(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            _commentRepository.Insert(comment);
            return new SuccessResult(string.Format(Messages.SuccessfulInsert, nameof(Picture)));
        }

        public IResult UpdateComment(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            _commentRepository.Update(comment);
            return new SuccessResult(string.Format(Messages.SuccessfulUpdate, nameof(Picture)));
        }

        public IResult DeleteComment(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            _commentRepository.Delete(comment);
            return new SuccessResult(string.Format(Messages.SuccessfulDelete, nameof(Picture)));
        }
    }
}
