using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.Data.Interfaces;
using project.Service.DTOs;
using project.Service.Interfaces;

namespace project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        [Route("GetAllGuests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<GuestDTO> GetGuests()
        {
            var guests = _guestService.GetGuests();

            return guests;
        }

        [HttpGet]
        [Route("GetGuestById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public GuestDTO GetGuestById(int id)
        {
            var Guest = _guestService.GetGuestById(id);

            return Guest;
        }


        [HttpPost]
        [Route("AddGuest")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GuestDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] GuestDTO guest)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            try
            {
                var newGuest = _guestService.AddGuest(guest);
                return Created("", $"Guest with id {newGuest.Id} has been created!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateGuest/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Put([FromRoute] int id, [FromBody] GuestDTO Guest)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            GuestDTO guestExists = _guestService.GetGuestById(Guest.Id);

            if (guestExists == null)
                return BadRequest("Guest with that id does not exist!");

            try
            {
                Guest.Id = id;
                var result = _guestService.UpdateGuest(Guest);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("RemoveGuest/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            GuestDTO guestExist = _guestService.GetGuestById(id);

            if (guestExist != null)
            {
                return Ok(_guestService.DeleteGuest(id));
            }
            else
            {
                return BadRequest("Guest with that id does not exist!");
            }
        }

    }
}
