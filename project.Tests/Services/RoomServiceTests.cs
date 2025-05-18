
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
    public class RoomServiceTests
    {
        IRoomRepository roomRepo;
        IMapper mapper;

        Mock<IRoomRepository> roomRepoMock = new Mock<IRoomRepository>();
        Mock<IMapper> mapperMock = new Mock<IMapper>();

        Room room;
        RoomDTO roomDTO;

        private void SetupMocks()
        {
            roomRepo = roomRepoMock.Object;
            mapper = mapperMock.Object;
        }

        private Room GetRoom() => new Room { Id = 1, Number = 5, Floor = 3, Type = "Suite" };
        private RoomDTO GetRoomDTO() => new RoomDTO { Id = 1, Number = 5, Floor = 3, Type = "Suite" };

        [Fact]
        public void AddRoom_WithValidRoom_ReturnsRoomDTO()
        {
            SetupMocks();
            room = GetRoom();
            roomDTO = GetRoomDTO();

            roomRepoMock.Setup(r => r.AddRoom(It.IsAny<Room>()));
            mapperMock.Setup(m => m.Map<Room>(roomDTO)).Returns(room);
            mapperMock.Setup(m => m.Map<RoomDTO>(room)).Returns(roomDTO);

            var service = new RoomService(roomRepo, mapper);
            var result = service.AddRoom(roomDTO);

            Assert.NotNull(result);
            Assert.Equal(5, result.Number);
        }

        [Fact]
        public void GetRoomById_WithValidId_ReturnsRoomDTO()
        {
            SetupMocks();
            room = GetRoom();
            roomDTO = GetRoomDTO();

            roomRepoMock.Setup(r => r.GetRoomById(1)).Returns(room);
            mapperMock.Setup(m => m.Map<RoomDTO>(room)).Returns(roomDTO);

            var service = new RoomService(roomRepo, mapper);
            var result = service.GetRoomById(1);

            Assert.NotNull(result);
            Assert.Equal("Suite", result.Type);
        }

        [Fact]
        public void DeleteRoom_WithNonExistentRoom_ReturnsFalse()
        {
            SetupMocks();
            roomRepoMock.Setup(r => r.GetRoomById(99)).Returns((Room)null);

            var service = new RoomService(roomRepo, mapper);
            var result = service.DeleteRoom(99);

            Assert.False(result);
        }
    }
}
