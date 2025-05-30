using AutoMapper;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using BLL.FacadePattern;
namespace PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly IMapper _mapper;

        public ClientsController(HotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientPL>> GetClients()
        {
            var clients = _hotelService.GetAllClients();
            return Ok(_mapper.Map<List<ClientPL>>(clients));
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<ClientPL>> Search([FromQuery] string? name, [FromQuery] string? surname)
        {
            var clients = _hotelService.SearchClients(name ?? "", surname ?? "");
            return Ok(_mapper.Map<List<ClientPL>>(clients));
        }

        [HttpGet("active-bookings")]
        public ActionResult<IEnumerable<ClientPL>> GetClientsWithActiveBookings()
        {
            var clients = _hotelService.GetClientsWithActiveBookings();
            return Ok(_mapper.Map<List<ClientPL>>(clients));
        }


        [HttpPost]
        public IActionResult AddClient([FromBody] ClientPL dto)
        {
            var model = _mapper.Map<ClientBLLModel>(dto);
            _hotelService.AddClient(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            _hotelService.DeleteClient(id);
            return Ok();
        }
    }
}
