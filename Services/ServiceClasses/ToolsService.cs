using Microsoft.AspNetCore.Http;

namespace Services.ServiceClasses
{
    public class ToolsService
    {

        public async Task<string> SaveImageOnDiskAsync(string imgbase64)
        {
            var path = GetNewImgPath();
            byte[] buffer = Convert.FromBase64String(imgbase64);

            using (var stream = new MemoryStream(buffer))
            {
                CompressImage(stream, path);
            }

            return Path.GetFileName(path);
        }

        public async Task<string> SaveImageOnDiskAsync(IFormFile image)
        {
            var path = GetNewImgPath();
            
            using (var stream = new MemoryStream())
            {
                image.CopyTo(stream);
                stream.Position = 0L;
                CompressImage(stream, path);
            }

            return Path.GetFileName(path);
        }

        private string GetNewImgPath()
        {
            var fileName = Path.GetRandomFileName() + ".jpeg";
            return Path.Combine(new[] { Directory.GetCurrentDirectory(), "images", fileName });
        }

        public void CompressImage(Stream stream, string filePath)
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
