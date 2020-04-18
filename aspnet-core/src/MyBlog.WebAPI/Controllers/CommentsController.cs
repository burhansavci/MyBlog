using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Dtos;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public IActionResult GetComments()
        {
            return Ok(_commentService.GetComments());
        }

        [HttpGet("{articleId}")]
        public IActionResult GetCommentById(int articleId)
        {
            return Ok(_commentService.GetCommentsByArticleId(articleId));
        }

        [HttpPost]
        public IActionResult AddComment(CommentDto commentDto)
        {
            return Ok(_commentService.InsertComment(commentDto));
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateComment(CommentDto commentDto)
        {
            return Ok(_commentService.UpdateComment(commentDto));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteCategory(CommentDto commentDto)
        {
            return Ok(_commentService.DeleteComment(commentDto));
        }
    }
}