using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Dtos;

namespace MyBlog.WebAPI.Controllers
{
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IArticleService _articleService;
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [Route("api/{languageCode}/[controller]/{pageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetArticles(string languageCode, int pageNumber, int pageSize)
        {
            return Ok(_articleService.GetArticles(languageCode, pageNumber, pageSize));
        }

        [Route("api/{languageCode}/[controller]/[action]/{id}")]
        [HttpGet]
        public IActionResult GetArticleById(int id, string languageCode)
        {
            return Ok(_articleService.GetArticleById(languageCode, id));
        }

        [Route("api/{languageCode}/[controller]/[action]/{categoryId}/{pageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetArticlesByCategoryId(int categoryId, string languageCode, int pageNumber, int pageSize)
        {
            return Ok(_articleService.GetArticlesByCategoryId(languageCode, categoryId, pageNumber, pageSize));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpPost]
        public IActionResult AddArticle(ArticleDto articleDto)
        {
            return Ok(_articleService.InsertArticle(articleDto));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpPut]
        public IActionResult UpdateArticle(ArticleDto articleDto)
        {
            return Ok(_articleService.UpdateArticle(articleDto));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteArticle(ArticleDto articleDto)
        {
            return Ok(_articleService.DeleteArticle(articleDto));
        }
    }
}