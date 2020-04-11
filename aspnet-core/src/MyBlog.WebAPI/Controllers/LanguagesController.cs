using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult AddLanguage(LanguageDto languageDto)
        {
            return Ok(_languageService.InsertLanguage(languageDto));
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateLanguage(LanguageDto languageDto)
        {
            return Ok(_languageService.UpdateLanguage(languageDto));
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteCategory(LanguageDto languageDto)
        {
            return Ok(_languageService.DeleteLanguage(languageDto));
        }
    }
}