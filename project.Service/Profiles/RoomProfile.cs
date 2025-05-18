using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.Data.Entities;
using project.Service.DTOs;

namespace project.Service.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, opt => opt.Ignore());

        }
    }
}
