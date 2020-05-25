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


        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetArticles()
        {
            return Ok(_articleService.GetArticles());
        }

        [Route("api/{languageCode}/[controller]/{pageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetArticlesByLanguageCode(string languageCode, int pageNumber, int pageSize)
        {
            return Ok(_articleService.GetArticlesByLanguageCode(languageCode, pageNumber, pageSize));
        }

        [Route("api/{languageCode}/[controller]/{id}")]
        [HttpGet]
        public IActionResult GetArticleById(int id, string languageCode)
        {
            return Ok(_articleService.GetArticleById(languageCode, id));
        }

        [Route("api/{languageCode}/[controller]/archive")]
        [HttpGet]
        public IActionResult GetArticlesArchive(string languageCode)
        {
            return Ok(_articleService.GetArticlesArchive(languageCode));
        }

        [Route("api/{languageCode}/[controller]/archive/{year}/{pageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetArticlesByYear(string languageCode, int year, int pageNumber, int pageSize)
        {
            return Ok(_articleService.GetArticlesByYear(languageCode, year, pageNumber, pageSize));
        }

        [Route("api/{languageCode}/[controller]/archive/{year}/{month}/{pageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetArticlesByYearAndMonth(string languageCode, int year, int month, int pageNumber, int pageSize)
        {
            return Ok(_articleService.GetArticlesByYearAndMonth(languageCode, year, month, pageNumber, pageSize));
        }

        [Route("api/{languageCode}/[controller]/{categoryId}/{pageNumber}/{pageSize}")]
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