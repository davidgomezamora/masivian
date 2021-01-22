using ApplicationCore.DTO.Bet;
using ApplicationCore.Helpers;
using ApplicationCore.ResourceParameters;
using AutoMapper;
using Common;
using Infraestructure.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.BetService
{
    public class BetService : BaseService<Bet, BetDto, BetForAdditionDto, BetForUpdateDto, BetForSortingDto, BetResourceParameters>, IBetService
    {
        public BetService(IRepository<Bet> repository, IMapper mapper) : base(repository, mapper) {
            this.PathRelatedEntities = new List<string> { "State", "Roulette" };
        }

        public async Task<bool> Close(Guid id)
        {
            return await this.UpdateAsync(id, new BetForUpdateDto() { StateId = new Guid("D38D2437-92B8-4C0C-BDE3-8C1029403F03") });
        }
    }
}
