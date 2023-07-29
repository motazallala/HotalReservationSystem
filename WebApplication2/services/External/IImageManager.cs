using System.IO;
using System.Threading.Tasks;

namespace Services.External
{
    public interface IImageManager
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
        Task DeleteImageAsync(string publicId);
        string GetPublicId(string publicId);

    }
}
