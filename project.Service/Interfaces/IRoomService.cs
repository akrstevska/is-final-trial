using System;
using project.Service.DTOs;

namespace project.Service.Interfaces;

public interface IRoomService
{
    List<RoomDTO> GetRooms();

    RoomDTO GetRoomById(int id);

    RoomDTO AddRoom(RoomDTO roomDTO);

    RoomDTO UpdateRoom(RoomDTO roomDTO);

    bool DeleteRoom(int id);
}
