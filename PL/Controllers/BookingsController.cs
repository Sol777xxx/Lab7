using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using BLL.FacadePattern;

namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly IMapper _mapper;

        public BookingsController(HotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingPL>> GetAllBookings()
        {
            var bookings = _hotelService.GetAllBookings();
            return Ok(_mapper.Map<List<BookingPL>>(bookings));
        }

        [HttpGet("active")]
        public ActionResult<IEnumerable<BookingPL>> GetActiveBookings()
        {
            var bookings = _hotelService.GetActiveBookings();
            return Ok(_mapper.Map<List<BookingPL>>(bookings));
        }

        [HttpPost]
        public IActionResult BookRoom([FromBody] BookingPL dto)
        {
            bool success = _hotelService.BookRoom(dto.RoomId, dto.ClientId, dto.StartDate, dto.EndDate);
            if (success)
                return Ok("Бронювання успішне");
            return BadRequest("Помилка при бронюванні");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            _hotelService.DeleteBooking(id);
            return Ok("Бронювання видалено");
        }

        [HttpPut("{id}/cancel")]
        public IActionResult CancelBooking(int id)
        {
            bool result = _hotelService.CancelBooking(id);
            return result ? Ok("Скасовано") : BadRequest("Неможливо скасувати");
        }

        [HttpPut("{id}/restore")]
        public IActionResult RestoreBooking(int id)
        {
            bool result = _hotelService.RestoreBooking(id);
            return result ? Ok("Відновлено") : BadRequest("Неможливо відновити");
        }
    }
}
