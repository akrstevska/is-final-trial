using System;
using project.Data.Entities;
namespace project.Data.Interfaces
{

    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Room GetRoomById(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room oldRoom, Room newRoom);
        bool DeleteRoom(Room room);

    }
}