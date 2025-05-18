using System;
using Microsoft.EntityFrameworkCore;
using project.Data.Entities;
using project.Data.Interfaces;

namespace project.Data.Repositories
{

    public class RoomRepository : IRoomRepository
    {
        private readonly TrialContext _dataContext;
        public RoomRepository(TrialContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddRoom(Room room)
        {
            _dataContext.Rooms.Add(room);
            _dataContext.SaveChanges();
        }

        public bool DeleteRoom(Room room)
        {
            try
            {
                _dataContext.Rooms.Remove(room);
                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Room GetRoomById(int id)
        {
            return _dataContext.Rooms.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _dataContext.Rooms.ToList();
        }

        public void UpdateRoom(Room oldRoom, Room newRoom)
        {
            newRoom.Id = oldRoom.Id;

            _dataContext.Entry(oldRoom).State = EntityState.Detached;
            _dataContext.Rooms.Attach(newRoom);
            _dataContext.Entry(newRoom).State = EntityState.Modified;

            _dataContext.SaveChanges();


        }
    }

}
