using ApplicationCore.DTO.Roulette;
using ApplicationCore.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services.RouletteService
{
    public interface IRouletteService : IBaseService<RouletteDto, RouletteForAdditionDto, RouletteForUpdateDto, RouletteForSortingDto, RouletteResourceParameters> { }
}
