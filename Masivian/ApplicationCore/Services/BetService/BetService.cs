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
    }
}
