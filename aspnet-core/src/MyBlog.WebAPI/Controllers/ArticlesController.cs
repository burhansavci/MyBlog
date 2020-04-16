using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Route("api/{languageCode}/[controller]")]
        [HttpGet]
        public IActionResult GetArticles(string languageCode)
        {
            return Ok(_articleService.GetArticles(languageCode));
        }

        [Route("api/{languageCode}/[controller]/[action]/{id}")]
        [HttpGet]
        public IActionResult GetArticleById(int id, string languageCode)
        {
            return Ok(_articleService.GetArticleById(id, languageCode));
        }

        [Route("api/{languageCode}/[controller]/[action]/{categoryId}")]
        [HttpGet]
        public IActionResult GetArticlesByCategoryId(int categoryId, string languageCode)
        {
            return Ok(_articleService.GetArticlesByCategoryId(categoryId, languageCode));
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