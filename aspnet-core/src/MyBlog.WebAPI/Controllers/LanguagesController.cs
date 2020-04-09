using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Dtos;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private ILanguageService _languageService;
        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public IActionResult GetLanguages()
        {
            return Ok(_languageService.GetLanguages());
        }

        [HttpGet("{languageCode}")]
        public IActionResult GetLanguageByCode(string languageCode)
        {
            return Ok(_languageService.GetLanguageByCode(languageCode));
        }

        [HttpPost]
        public IActionResult AddLanguage(LanguageDto languageDto)
        {
            return Ok(_languageService.InsertLanguage(languageDto));
        }

        [HttpPut]
        public IActionResult UpdateLanguage(LanguageDto languageDto)
        {
            return Ok(_languageService.UpdateLanguage(languageDto));
        }

        [HttpDelete]
        public IActionResult DeleteCategory(LanguageDto languageDto)
        {
            return Ok(_languageService.DeleteLanguage(languageDto));
        }
    }
}