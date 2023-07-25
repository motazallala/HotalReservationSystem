﻿using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;
using WebApplication2.services.Mapping;

namespace WebApplication2.services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly ApplicationDBContext _db;

        public RoomTypeService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task Add(RoomType roomType)
        {
            await _db.RoomTypes.AddAsync(roomType);
            await _db.SaveChangesAsync();
        }

        public int CountAllRoomType(string searchText)
        {
            return _db.RoomTypes.Where(x => x.Type.Contains(searchText)).Count();
        }

        public async Task Delete(int id)
        {
            var data = await _db.RoomTypes.FirstOrDefaultAsync(x => x.RoomTypeId == id);
            if (data != null)
            {
                _db.RoomTypes.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            var result = await _db.RoomTypes.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllRoomTypesPager<T>(string searchText)
        {
            IQueryable<RoomType> data = _db.RoomTypes;
            data = data.Where(x => x.Type.Contains(searchText));

            return await data.ProjectTo<T>().ToListAsync();
        }

        public async Task<RoomType> GetId(int id)
        {
            var data = await _db.RoomTypes.FirstOrDefaultAsync(x => x.RoomTypeId == id);
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public async Task Update(int id, RoomType roomType)
        {
            roomType.RoomTypeId = id;
            var roomTypeToChange = await _db.RoomTypes.FirstOrDefaultAsync(e => e.RoomTypeId == id);
            if (roomTypeToChange != null)
            {
                _db.RoomTypes.Update(roomTypeToChange);
                _db.SaveChanges();
            }
        }
    }
}