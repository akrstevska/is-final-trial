using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.Service.DTOs;

namespace project.Service.Interfaces
{
    public interface IGuestService
    {
        List<GuestDTO> GetGuests();

        GuestDTO GetGuestById(int id);


        GuestDTO AddGuest(GuestDTO guestDTO);

        GuestDTO UpdateGuest(GuestDTO guestDTO);

        bool DeleteGuest(int id);
    }
}
