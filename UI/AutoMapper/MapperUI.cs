using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using UI.Models;
using PL.Models;

namespace UI.AutoMapper
{
    public class MapperUI : Profile
    {
        public MapperUI()
        {
            // Client
            CreateMap<ClientPL, ClientUI>().ReverseMap();

            // Room
            CreateMap<RoomPL, RoomUI>().ReverseMap();
            CreateMap<RoomStatusPL, RoomStatusUI>().ReverseMap();
            CreateMap<CategoriesPL, CategoriesUI>().ReverseMap();

            // Booking
            CreateMap<BookingPL, BookingUI>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(dest => dest.RoomCategory, opt => opt.MapFrom(src => src.RoomCategory))
                .ReverseMap();
        }
    }
}


