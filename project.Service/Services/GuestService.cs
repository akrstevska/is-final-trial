using System;
using project.Service.DTOs;
using project.Service.Interfaces;
using project.Data.Entities;
using AutoMapper;
using project.Data.Interfaces;
namespace project.Service.Services
{

    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        private readonly IMapper _mapper;

        public GuestService(IGuestRepository guestRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public GuestDTO AddGuest(GuestDTO guestDTO)
        {
            var room = _roomRepository.GetRoomById(guestDTO.Room.Id);
            if (room == null)
                throw new ArgumentException($"Room with id {guestDTO.Room.Id} does not exist.");

            var newGuest = _mapper.Map<Guest>(guestDTO);
            newGuest.RoomId = guestDTO.Room.Id;
            newGuest.Room = null;

            _guestRepository.AddGuest(newGuest);
            return _mapper.Map<GuestDTO>(newGuest);
        }

        public bool DeleteGuest(int id)
        {
            var guest = _guestRepository.GetGuestById(id);
            if (guest != null)
            {
                return _guestRepository.DeleteGuest(guest);
            }
            return false;
        }

        public GuestDTO GetGuestById(int id)
        {
            var guest = _guestRepository.GetGuestById(id);
            return _mapper.Map<GuestDTO>(guest);
        }

        public List<GuestDTO> GetGuests()
        {
            var guests = _guestRepository.GetGuests();
            return _mapper.Map<List<GuestDTO>>(guests);
        }

        public GuestDTO UpdateGuest(GuestDTO guestDTO)
        {
            var existingGuest = _guestRepository.GetGuestById(guestDTO.Id);
            if (existingGuest == null)
                return null;

            var room = _roomRepository.GetRoomById(guestDTO.Room.Id);
            if (room == null)
                throw new ArgumentException($"Room with id {guestDTO.Room.Id} does not exist.");

            var newGuest = _mapper.Map<Guest>(guestDTO);
            newGuest.RoomId = guestDTO.Room.Id;
            newGuest.Room = null;

            _guestRepository.UpdateGuest(existingGuest, newGuest);
            return _mapper.Map<GuestDTO>(newGuest);
        }
    }
}
