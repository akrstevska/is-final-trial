using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data.Interfaces;
using project.Service.DTOs;
using project.Service.Interfaces;

namespace project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        [Route("GetAllRooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<RoomDTO> GetRooms()
        {
            var rooms = _roomService.GetRooms();

            return rooms;
        }

        [HttpGet]
        [Route("GetRoomById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public RoomDTO GetRoomById(int id)
        {
            var Room = _roomService.GetRoomById(id);

            return Room;
        }


        [HttpPost]
        [Route("AddRoom")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] RoomDTO Room)
        {
            if (ModelState.IsValid)
            {

                var newRoom = _roomService.AddRoom(Room);
                return Created("", $"Room with id {newRoom.Id} has been created!");

            }
            return UnprocessableEntity(ModelState);
        }

        [HttpPut]
        [Route("UpdateRoom/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Put([FromRoute] int id, [FromBody] RoomDTO Room)
        {
            if (ModelState.IsValid)
            {
                RoomDTO roomExists = _roomService.GetRoomById(Room.Id);

                if (roomExists != null)
                {
                    Room.Id = id;
                    var result = _roomService.UpdateRoom(Room);

                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest("Room with that id does not exist!");
                }
            }

            return UnprocessableEntity(ModelState);
        }

        [HttpDelete("RemoveRoom/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            RoomDTO roomExist = _roomService.GetRoomById(id);

            if (roomExist != null)
            {
                return Ok(_roomService.DeleteRoom(id));
            }
            else
            {
                return BadRequest("Room with that id does not exist!");
            }
        }

    }
}
