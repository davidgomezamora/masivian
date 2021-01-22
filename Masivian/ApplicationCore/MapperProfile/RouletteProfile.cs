using ApplicationCore.DTO.Roulette;
using AutoMapper;
using Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.MapperProfile
{
    public class RouletteProfile : Profile
    {
        public RouletteProfile()
        {
            CreateMap<Roulette, RouletteDto>()
                .ReverseMap();
            CreateMap<Roulette, RouletteForUpdateDto>()
                .ReverseMap();
            CreateMap<Roulette, RouletteForAdditionDto>()
                .ReverseMap();

            CreateMap<RouletteForSortingDto, Roulette>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
