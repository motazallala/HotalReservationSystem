using AutoMapper;

using WebApplication2.Data.Model;
using WebApplication2.Models.Escort;
using WebApplication2.Models.Reservation;
using WebApplication2.Models.Room;
using WebApplication2.Models.RoomImage;
using WebApplication2.Models.RoomType;

/// <summary>
/// View layer common helpers
/// </summary>
namespace Web.Common
{
    /// <summary>
    /// AutoMapper profile for registering maps
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RoomTypeInput, RoomType>();
            CreateMap<RoomType, RoomTypeInput>();

            CreateMap<RoomTypeViewModel, RoomType>();
            CreateMap<RoomType, RoomTypeViewModel>();

            CreateMap<RoomInputModel, Room>();
            CreateMap<Room, RoomInputModel>();

            CreateMap<RoomViewModel, Room>();
            CreateMap<Room, RoomViewModel>();

            CreateMap<RoomImage, RoomViewModel>();
            CreateMap<Reservation, ReservationViewModel>();

            CreateMap<Escort, EscortViewModel>();
            CreateMap<Room, Room>();
            CreateMap<RoomImage, RoomImageViewModel>();
            CreateMap<RoomImage, IFormFile>();
        }
    }
}