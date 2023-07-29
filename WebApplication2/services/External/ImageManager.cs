using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.External
{
    public class ImageManager : IImageManager
    {
        private readonly string cloudName;
        private readonly string apiKey;
        private readonly string apiSecret;

        public ImageManager(string cloudName, string apiKey, string apiSecret)
        {
            this.cloudName = cloudName;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }


        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {

            var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
            cloudinary.Api.Secure = true;

            // Check if the image file is empty
            if (imageFile == null || imageFile.Length == 0)
            {
                return "Error: Empty file";
            }

            // Create a unique filename for the image
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            using var stream = new MemoryStream();
            await imageFile.CopyToAsync(stream);

            // Reset the position of the memory stream to the beginning
            stream.Position = 0;

            // Make sure to create Folder in "Cloudinary" website.
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(imageName, stream),
                Format = "jpg",
                PublicId = imageName,
                UseFilename = true,
                Folder = "RoomImages"
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                // Handle the error if the image upload fails
                return "Error: " + uploadResult.Error.Message;
            }

            // Return the URL of the uploaded image from Cloudinary
            return uploadResult.SecureUrl.ToString();
        }
    }
}
