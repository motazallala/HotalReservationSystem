﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Services.External;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomImageService : IRoomImageService
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        private readonly ImageManager _imageManager;

        public RoomImageService(ApplicationDBContext db, IMapper mapper, ImageManager imageManager)
        {
            _db = db;
            _mapper = mapper;
            _imageManager = imageManager;
        }

        public async Task AddRange(int id, List<IFormFile> roomImages)
        {
            foreach (var imageFile in roomImages)
            {
                // Upload and add the new image to the database
                string imageUrl = await _imageManager.UploadImageAsync(imageFile);
                var newImage = new RoomImage
                {
                    ImageUrl = imageUrl,
                    RoomId = id
                };
                _db.RoomImages.Add(newImage);
            }
            await _db.SaveChangesAsync();
        }

        public async Task Remove(int roomImageId)
        {
            var removeRoomImage = await _db.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == roomImageId);

            if (removeRoomImage != null)
            {
                _db.RoomImages.Remove(removeRoomImage);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Update(int Id, RoomImage roomImage)
        {
            roomImage.RoomImageId = Id;
            var updateRoomImage = await _db.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == Id);

            if (roomImage != null)
            {
                _db.Update(roomImage);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<RoomImage>> GetAllRoomImage()
        {
            var allRoomImage = await _db.RoomImages.ToListAsync();
            return allRoomImage;
        }

        public async Task<IEnumerable<T>> GetRoomImage<T>(int id)
        {
            IQueryable<RoomImage> data = _db.RoomImages;
            data = data.Where(x => x.RoomId == id);

            return await data.ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}