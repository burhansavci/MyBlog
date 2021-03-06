﻿using Microsoft.AspNetCore.Authorization;
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


        [Route("api/[controller]/{startPageNumber}/{endPageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetArticles(int startPageNumber, int endPageNumber, int pageSize)
        {
            return Ok(_articleService.GetArticles(startPageNumber, endPageNumber, pageSize));
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
        public IActionResult AddArticle([FromForm] ArticleForCreationDto articleForCreationDto)
        {
            return Ok(_articleService.InsertArticle(articleForCreationDto));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpPut]
        public IActionResult UpdateArticle([FromForm] ArticleForUpdateDto articleForUpdateDto)
        {
            return Ok(_articleService.UpdateArticle(articleForUpdateDto));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteArticle(ArticleForDeleteDto articleForDeleteDto)
        {
            return Ok(_articleService.DeleteArticle(articleForDeleteDto));
        }
    }
}