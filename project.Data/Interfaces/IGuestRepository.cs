using System;
using project.Data.Entities;
namespace project.Data.Interfaces
{

    public interface IGuestRepository
    {
        IEnumerable<Guest> GetGuests();
        Guest GetGuestById(int id);
        void AddGuest(Guest guest);
        void UpdateGuest(Guest oldGuest, Guest newGuest);
        bool DeleteGuest(Guest guest);

    }
}
