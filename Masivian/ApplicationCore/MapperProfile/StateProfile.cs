using ApplicationCore.DTO.State;
using AutoMapper;
using Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.MapperProfile
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<Status, StateDto>()
                .ReverseMap();
        }
    }
}
