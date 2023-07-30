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

        public async Task DeleteImageAsync(string publicId)
        {
            var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);

            // Use the Cloudinary API to delete the image using its public ID
            string getPublicId = GetPublicId(publicId);
            string newPublicId = "RoomImages/"+getPublicId;

            var deleteParams = new DeletionParams(newPublicId)
            {
                Invalidate = true,
                Type = "upload",
                ResourceType = ResourceType.Image
            };

            await cloudinary.DestroyAsync(deleteParams);
        }

        public string GetPublicId(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));
            }

            // Find the last occurrence of '/'
            int lastSlashIndex = imageUrl.LastIndexOf('/');
            if (lastSlashIndex == -1 || lastSlashIndex >= imageUrl.Length - 1)
            {
                throw new ArgumentException("Invalid image URL.", nameof(imageUrl));
            }

            // Get the substring after the last '/'
            string publicIdWithFormat = imageUrl.Substring(lastSlashIndex + 1);

            // Find the last occurrence of '.' to remove the file extension
            int lastDotIndex = publicIdWithFormat.LastIndexOf('.');
            if (lastDotIndex == -1 || lastDotIndex == 0)
            {
                throw new ArgumentException("Invalid image URL.", nameof(imageUrl));
            }

            // Get the substring before the last '.' to get the public ID
            string publicId = publicIdWithFormat.Substring(0, lastDotIndex);

            return publicId;
        }
    }
}
