﻿using ApplicationCore.DTO.Bet;
using AutoMapper;
using Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.MapperProfile
{
    public class BetProfile : Profile
    {
        public BetProfile()
        {
            CreateMap<Bet, BetDto>()
                .ReverseMap();
            CreateMap<Bet, BetForUpdateDto>()
                .ReverseMap();
            CreateMap<Bet, BetForAdditionDto>()
                .ReverseMap();

            CreateMap<BetForSortingDto, Bet>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Money,
                opt => opt.MapFrom(src => src.Money))
                .ForMember(dest => dest.Prize,
                opt => opt.MapFrom(src => src.Prize))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
