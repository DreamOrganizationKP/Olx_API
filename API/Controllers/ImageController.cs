using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.ServiceClasses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ToolsService _toolsService;
        public ImageController(ToolsService toolsService)
        {
            _toolsService = toolsService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequestVM<IFormFile> model)
        {
            try
            {           
                if(model.Image == null)
                {
                    return BadRequest(new SimpleResponseVM()
                    {
                        IsSuccess = false
                    });
                }

                var fileName = await _toolsService.SaveImageOnDiskAsync(model.Image);

                return Ok(new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = fileName
                });
            }
            catch (Exception)
            {
                return BadRequest(new SimpleResponseVM()
                {
                    IsSuccess = false
                });
            }
        }

        [HttpPost("upload/base64")]
        public async Task<IActionResult> UploadImageBase64([FromBody] UploadImageRequestVM<string> model)
        {
            try
            {
                if (model.Image == null)
                {
                    return BadRequest(new SimpleResponseVM()
                    {
                        IsSuccess = false
                    });
                }

                var fileName = await _toolsService.SaveImageOnDiskAsync(model.Image);

                return BadRequest(new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = fileName
                });
            }
            catch (Exception)
            {
                return BadRequest(new SimpleResponseVM()
                {
                    IsSuccess = false
                });
            }
        }

        
    }
}
