using AutoMapper;

using WebApplication2.Data.Model;
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
        }
    }
}