using ApplicationCore.DTO.Roulette;
using ApplicationCore.Helpers;
using ApplicationCore.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.RouletteService
{
    public interface IRouletteService : IBaseService<RouletteDto, RouletteForAdditionDto, RouletteForUpdateDto, RouletteForSortingDto, RouletteResourceParameters> {
        public Task<bool> SwitchStateAsync(Guid id);
        public Task<bool> ValidateAsync(Guid id);
    }
}
