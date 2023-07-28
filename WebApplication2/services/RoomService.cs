using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Services.External;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        private readonly IImageManager _imageManager;

        public RoomService(ApplicationDBContext db, IMapper mapper, IImageManager imageManager)
        {
            _db = db;
            _mapper = mapper;
            _imageManager = imageManager;
        }

        public async Task Add(Room room, List<IFormFile> roomImages)
        {
            // Save the new room to the database
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();

            // Process and add room images
            if (roomImages != null && roomImages.Any())
            {
                foreach (var imageFile in roomImages)
                {
                    // Process the image file and save it using Cloudinary
                    string imageUrl = await _imageManager.UploadImageAsync(imageFile);

                    // Create a new RoomImage object and add it to the room's collection of images
                    var newImage = new RoomImage
                    {
                        ImageUrl = imageUrl,
                        RoomId = room.RoomId
                    };

                    // Save the new room image to the database
                    await _db.RoomImages.AddAsync(newImage);
                }
                await _db.SaveChangesAsync();
            }
        }


        public Task Update(int id, Room room)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetAllRoom()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetSearchResults<T>(bool availableOnly = false, int? minCapacity = null, int? type = null)
        {
            IQueryable<Room> result = _db.Rooms;
            if (availableOnly)
            {
                result = result.Where(x => !x.Reservations.Any(y => y.CheckIn <= DateTime.Today
                                                                 && y.CheckOut > DateTime.Today));
            }

            if (type != null && type > 0)
            {
                result = result.Where(x => x.RoomTypeId == type);
            }

            if (minCapacity != null && minCapacity > 0)
            {
                result = result.Where(x => x.Capacity >= minCapacity.Value);
            }
            return await result.OrderBy(x => x.RoomNumber).ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<int> GetMaxCapacity()
        {
            return await _db.Rooms.AsNoTracking().
                           OrderByDescending(x => x.Capacity).
                           Select(x => x.Capacity).
                           FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRoomTypeList<T>()
        {
            return await _db.RoomTypes.ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<Room> GetId(int id)
        {
            throw new NotImplementedException();
        }

    }
}