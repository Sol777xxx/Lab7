using AutoMapper;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using BLL.FacadePattern;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly IMapper _mapper;

        public RoomsController(HotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomPL>> GetAllRooms()
        {
            var rooms = _hotelService.GetAllRooms();
            return Ok(_mapper.Map<List<RoomPL>>(rooms));
        }

        [HttpGet("available")]
        public ActionResult<IEnumerable<RoomPL>> GetAvailableRooms()
        {
            var rooms = _hotelService.GetAvailableRooms();
            return Ok(_mapper.Map<List<RoomPL>>(rooms));
        }

        [HttpPost]
        public IActionResult AddRoom([FromBody] RoomPL dto)
        {
            var model = _mapper.Map<RoomBLLModel>(dto);
            _hotelService.AddRoom(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            _hotelService.DeleteRoom(id);
            return Ok();
        }

        [HttpPut("{id}/status")]
        public IActionResult ChangeStatus(int id, [FromQuery] RoomStatusPL status)
        {
            var statusEnum = _mapper.Map<RoomStatus>(status);
            _hotelService.ChangeRoomStatus(id, statusEnum);
            return Ok();
        }

        [HttpGet("{id}/price")]
        public IActionResult GetRoomPrice(int id)
        {
            var room = _hotelService.GetRoomById(id);
            if (room == null)
                return NotFound();

            return Ok(_hotelService.GetRoomPrice(room));
        }
    }
}
