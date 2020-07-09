using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Dtos;

namespace MyBlog.WebAPI.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("api/[controller]/{startPageNumber}/{endPageNumber}/{pageSize}")]
        [HttpGet]
        public IActionResult GetCategories(int startPageNumber, int endPageNumber, int pageSize)
        {
            return Ok(_categoryService.GetCategories(startPageNumber, endPageNumber, pageSize));
        }

        [Route("api/{languageCode}/[controller]/")]
        [HttpGet]
        public IActionResult GetCategoriesByLanguage(string languageCode)
        {
            return Ok(_categoryService.GetCategoriesByLanguage(languageCode));
        }

        [Route("api/{languageCode}/categories/{id}")]
        [HttpGet]
        public IActionResult GetCategoryByIdAndLanguage(int id, string languageCode)
        {
            return Ok(_categoryService.GetCategoryByIdAndLanguage(id, languageCode));
        }
        [Route("api/categories/{id}")]
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_categoryService.GetCategoryById(id));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(CategoryForCreationDto categoryForCreationDto)
        {
            return Ok(_categoryService.InsertCategory(categoryForCreationDto));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpPut]
        public IActionResult UpdateCategory(CategoryForUpdateDto categoryForUpdateDto)
        {
            return Ok(_categoryService.UpdateCategory(categoryForUpdateDto));
        }

        [Route("api/[controller]")]
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteCategory(CategoryDto categoryDto)
        {
            return Ok(_categoryService.DeleteCategory(categoryDto));
        }
    }
}