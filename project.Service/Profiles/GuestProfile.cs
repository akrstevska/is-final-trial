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
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<Guest, GuestDTO>()
                .ReverseMap()
                .ForMember(d => d.Id, opt => opt.Ignore());

        }
    }
}
