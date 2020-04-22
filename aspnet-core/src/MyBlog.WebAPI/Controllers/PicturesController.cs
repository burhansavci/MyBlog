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
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private IPictureService _pictureService;
        public PicturesController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetPictureById(int id)
        {
            return Ok(_pictureService.GetPictureById(id));
        }

        [HttpGet("[action]/{articleId}")]
        public IActionResult GetPicturesByArticleId(int articleId)
        {
            return Ok(_pictureService.GetPicturesByArticleId(articleId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddPicture([FromForm]PictureForCreationDto pictureDto)
        {
            return Ok(_pictureService.InsertPictureForArticle(pictureDto));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeletePicture(PictureForDeleteDto pictureDto)
        {
            return Ok(_pictureService.DeletePicture(pictureDto));
        }
    }
}