using ApplicationCore.DTO.Roulette;
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

namespace ApplicationCore.Services.RouletteService
{
    public class RouletteService : BaseService<Roulette, RouletteDto, RouletteForAdditionDto, RouletteForUpdateDto, RouletteForSortingDto, RouletteResourceParameters>, IRouletteService
    {
        public RouletteService(IRepository<Roulette> repository, IMapper mapper) : base(repository, mapper) {
            this.PathRelatedEntities = new List<string> { "State", "Bets" };
        }

        public async Task<bool> Open(Guid id)
        {
            return await this.UpdateAsync(id, new RouletteForUpdateDto() { StateId = new Guid("42290071-BD11-4089-9483-45612311FF52") });
        }

        public async Task<bool> Close(Guid id)
        {
            return await this.UpdateAsync(id, new RouletteForUpdateDto() { StateId = new Guid("D38D2437-92B8-4C0C-BDE3-8C1029403F03") });
        }
    }
}
