using ApplicationCore.DTO.Bet;
using ApplicationCore.Helpers;
using ApplicationCore.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.BetService
{
    public interface IBetService : IBaseService<BetDto, BetForAdditionDto, BetForUpdateDto, BetForSortingDto, BetResourceParameters> {

        public Task<bool> Close(Guid id);
    }
}
