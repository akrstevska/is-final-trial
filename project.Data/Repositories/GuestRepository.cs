using System;
using Microsoft.EntityFrameworkCore;
using project.Data.Entities;
using project.Data.Interfaces;

namespace project.Data.Repositories
{

    public class GuestRepository : IGuestRepository
    {

        private readonly TrialContext _dataContext;

        public GuestRepository(TrialContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddGuest(Guest guest)
        {
            _dataContext.Guests.Add(guest);
            _dataContext.SaveChanges();
        }

        public bool DeleteGuest(Guest guest)
        {
            try
            {
                _dataContext.Guests.Remove(guest);
                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Guest GetGuestById(int id)
        {
            return _dataContext.Guests.Include(guests => guests.Room).FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Guest> GetGuests()
        {
            return _dataContext.Guests.Include(g => g.Room);
        }

        public void UpdateGuest(Guest oldGuest, Guest newGuest)
        {
            newGuest.Id = oldGuest.Id;

            _dataContext.Entry(oldGuest).State = EntityState.Detached;
            _dataContext.Guests.Attach(newGuest);
            _dataContext.Entry(newGuest).State = EntityState.Modified;

            _dataContext.SaveChanges();

        }
    }
}
