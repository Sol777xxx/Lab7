
using AutoMapper;
using PL.Models;
using BLL.Models;

namespace PL.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            // Client
            CreateMap<ClientBLLModel, ClientDto>().ReverseMap();

            // Room
            CreateMap<RoomBLLModel, RoomDto>().ReverseMap();
            CreateMap<RoomStatus, RoomStatusDto>().ReverseMap();
            CreateMap<Categories, CategoriesDto>().ReverseMap();

            // Booking
            CreateMap<BookingBLLModel, BookingDto>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.Id))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room.Id))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => $"{src.Client.Name} {src.Client.SurName}"))
                .ForMember(dest => dest.RoomCategory, opt => opt.MapFrom(src => src.Room.Category.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Client, opt => opt.Ignore()) 
                .ForMember(dest => dest.Room, opt => opt.Ignore());
        }
    }

}
