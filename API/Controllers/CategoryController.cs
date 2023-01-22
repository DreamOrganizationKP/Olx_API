using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.ServiceClasses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();

            if(result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryRequestVM model)
        {
            var result = await _categoryService.CreateAsync(model);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> CreateAsync([FromQuery] string id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
