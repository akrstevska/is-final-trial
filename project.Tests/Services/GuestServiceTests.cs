
using AutoMapper;
using Moq;
using project.Data.Entities;
using project.Data.Interfaces;
using project.Service.DTOs;
using project.Service.Services;
using Xunit;
using System.Collections.Generic;

namespace project.Tests.Services
{
    public class GuestServiceTests
    {
        IGuestRepository guestRepo;
        IRoomRepository roomRepo;
        IMapper mapper;

        Mock<IGuestRepository> guestRepoMock = new Mock<IGuestRepository>();
        Mock<IRoomRepository> roomRepoMock = new Mock<IRoomRepository>();
        Mock<IMapper> mapperMock = new Mock<IMapper>();

        Guest guest;
        GuestDTO guestDTO;
        Room room;

        private void SetupMocks()
        {
            guestRepo = guestRepoMock.Object;
            roomRepo = roomRepoMock.Object;
            mapper = mapperMock.Object;
        }

        private Guest GetGuest() => new Guest { Id = 1, FirstName = "Angela", LastName = "Krstevska", RoomId = 1 };
        private GuestDTO GetGuestDTO() => new GuestDTO { Id = 1, FirstName = "Angela", LastName = "Krstevska", Room = new RoomDTO { Id = 1 } };
        private Room GetRoom() => new Room { Id = 1, Number = 5, Floor = 3, Type = "Suite" };

        [Fact]
        public void AddGuest_WithValidRoom_ReturnsGuestDTO()
        {
            SetupMocks();
            guest = GetGuest();
            guestDTO = GetGuestDTO();
            room = GetRoom();

            roomRepoMock.Setup(r => r.GetRoomById(1)).Returns(room);
            guestRepoMock.Setup(r => r.AddGuest(It.IsAny<Guest>()));
            mapperMock.Setup(m => m.Map<Guest>(guestDTO)).Returns(guest);
            mapperMock.Setup(m => m.Map<GuestDTO>(guest)).Returns(guestDTO);

            var service = new GuestService(guestRepo, roomRepo, mapper);
            var result = service.AddGuest(guestDTO);

            Assert.NotNull(result);
            Assert.Equal(guestDTO.FirstName, result.FirstName);
        }

        [Fact]
        public void GetGuestById_WithValidId_ReturnsGuestDTO()
        {
            SetupMocks();
            guest = GetGuest();
            guestDTO = GetGuestDTO();

            guestRepoMock.Setup(r => r.GetGuestById(1)).Returns(guest);
            mapperMock.Setup(m => m.Map<GuestDTO>(guest)).Returns(guestDTO);

            var service = new GuestService(guestRepo, roomRepo, mapper);
            var result = service.GetGuestById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void DeleteGuest_WithExistingGuest_ReturnsTrue()
        {
            SetupMocks();
            guest = GetGuest();

            guestRepoMock.Setup(r => r.GetGuestById(1)).Returns(guest);
            guestRepoMock.Setup(r => r.DeleteGuest(guest)).Returns(true);

            var service = new GuestService(guestRepo, roomRepo, mapper);
            var result = service.DeleteGuest(1);

            Assert.True(result);
        }
    }
}
