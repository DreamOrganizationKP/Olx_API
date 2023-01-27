using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
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

                var fileName = Path.GetRandomFileName() + ".jpeg";
                var path = Path.Combine(new[] { Directory.GetCurrentDirectory(), "images", fileName });

                using (var stream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(stream);
                    stream.Position = 0L;
                    CompressImage(stream, path);
                }

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

                var fileName = Path.GetRandomFileName() + ".jpeg";
                var path = Path.Combine(new[] { Directory.GetCurrentDirectory(), "images", fileName });
                byte[] buffer = Convert.FromBase64String(model.Image);

                using (var stream = new MemoryStream(buffer))
                {
                    CompressImage(stream, path);
                }

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

        private void CompressImage(Stream stream, string filePath)
        {
            var settings = new PhotoSauce.MagicScaler.ProcessImageSettings()
            {
                EncoderOptions = new PhotoSauce.MagicScaler.JpegEncoderOptions()
                {
                    Quality = 65,
                    Subsample = PhotoSauce.MagicScaler.ChromaSubsampleMode.Default
                },
            };

            using var ms = new FileStream(filePath, FileMode.CreateNew);
            PhotoSauce.MagicScaler.MagicImageProcessor.ProcessImage(stream, ms, settings);
        }
    }
}
