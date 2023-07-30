using System.IO;
using System.Threading.Tasks;

namespace Services.External
{
    public interface IImageManager
    {
        public Task<string> UploadImageAsync(IFormFile imageFile);
        public Task DeleteImageAsync(string publicId);
        public string GetPublicId(string publicId);

    }
}
