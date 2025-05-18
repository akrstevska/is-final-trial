using System;
using project.Service.DTOs;
using project.Service.Interfaces;
using project.Data.Entities;
using AutoMapper;
using project.Data.Interfaces;
namespace project.Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public RoomDTO AddRoom(RoomDTO roomDTO)
        {
            Room newRoom = _mapper.Map<Room>(roomDTO);
            _roomRepository.AddRoom(newRoom);

            return _mapper.Map<RoomDTO>(newRoom);
        }

        public bool DeleteRoom(int id)
        {
            var Room = _roomRepository.GetRoomById(id);
            if (Room != null)
            {
                return _roomRepository.DeleteRoom(Room);
            }
            return false;
        }

        public RoomDTO GetRoomById(int id)
        {
            var Room = _roomRepository.GetRoomById(id);
            return _mapper.Map<RoomDTO>(Room);
        }

        public List<RoomDTO> GetRooms()
        {
            var rooms = _roomRepository.GetRooms();
            return _mapper.Map<List<RoomDTO>>(rooms);
        }

        public RoomDTO UpdateRoom(RoomDTO roomDTO)
        {
            var existingRoom = _roomRepository.GetRoomById(roomDTO.Id);

            if (existingRoom == null)
                return null;

            var newRoom = _mapper.Map<Room>(roomDTO);

            _roomRepository.UpdateRoom(existingRoom, newRoom);

            return _mapper.Map<RoomDTO>(newRoom);
        }
    }
}
