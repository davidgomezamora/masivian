using ApplicationCore.DTO.Roulette;
using ApplicationCore.Helpers;
using ApplicationCore.ResourceParameters;
using AutoMapper;
using Infraestructure.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services.RouletteService
{
    public class RouletteService : BaseService<Roulette, RouletteDto, RouletteForAdditionDto, RouletteForUpdateDto, RouletteForSortingDto, RouletteResourceParameters>
    {
        public RouletteService(IRepository<Roulette> repository, IMapper mapper) : base(repository, mapper) { }


    }
}
