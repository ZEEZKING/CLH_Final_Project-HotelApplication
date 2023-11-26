using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromForm] CreateRoomRequestModel model)
        {
            var room = await _roomServices.Create(model);
            if(room.Sucesss == false)
            {
                return BadRequest(room);
            }
            return Ok(room);
        }
        [HttpPut("UpdateRoom/{id}")]
        public async Task<IActionResult> UpdateRoom([FromForm] UpdateRoomRequestModel model,[FromRoute]int id) 
        {
            var room = await _roomServices.UpdateRoomAsync(model,id);
            if(room.Sucesss == false)
            {
                return BadRequest(room);
            }
            return Ok(room);
        }
        [HttpGet("GetRoom/{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _roomServices.GetRoomByIdAsync(id);
            if (room.Sucesss == false)
            {
                return BadRequest(room);
            }
            return Ok(room);
        }

        [HttpGet("GetAllRooms")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomServices.GetAllRooms();
            if(rooms.Sucesss == false)
            {
                return BadRequest(rooms);
            }
            return Ok(rooms);
        }

        [HttpGet("GetAllAvailableRooms")]
        public async Task<IActionResult> GetAllAvailableRooms()
        {
            var rooms = await _roomServices.GetAllAvailableRoom();
            if(rooms.Sucesss == false)
            {
                return BadRequest(rooms);
            }
            return Ok(rooms);
        }

        [HttpGet("GetAllUnAvailableRooms")]
        public async Task<IActionResult> GetUnAllAvailableRooms()
        {
            var rooms = await _roomServices.GetUnAvailableRoom();
            if (rooms.Sucesss == false)
            {
                return BadRequest(rooms);
            }
            return Ok(rooms);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomServices.DeleteAsync(id);
            if(room.Sucesss == false)
            {
                return BadRequest(room);
            }
            return Ok(room);
        }


    }
}
