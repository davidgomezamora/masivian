using ApplicationCore.DTO.Bet;
using ApplicationCore.DTO.Roulette;
using ApplicationCore.Helpers;
using ApplicationCore.ResourceParameters;
using ApplicationCore.Services.BetService;
using AutoMapper;
using Common;
using Infraestructure.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.RouletteService
{
    public class RouletteService : BaseService<Roulette, RouletteDto, RouletteForAdditionDto, RouletteForUpdateDto, RouletteForSortingDto, RouletteResourceParameters>, IRouletteService
    {
        private readonly IBetService _betService;
        public RouletteService(IBetService betService, IRepository<Roulette> repository, IMapper mapper) : base(repository, mapper) {
            this._betService = betService ??
                throw new ArgumentNullException(nameof(betService));

            this.SetPathRelatedEntities();
        }

        public async Task<bool> SwitchStateAsync(Guid id)
        {
            // Get roulette information, including betting
            RouletteDto rouletteDto = this.Mapper.Map<RouletteDto>(await this.GetAsync(null, null, id));

            return rouletteDto.State.IsOpen ? await CloseAsync(rouletteDto) : await OpenAsync(id);
        }

        private async Task<bool> OpenAsync(Guid id)
        {
            return await this.UpdateAsync(id, new RouletteForUpdateDto() { StateId = new Guid("42290071-BD11-4089-9483-45612311FF52") });
        }

        private async Task<bool> CloseAsync(RouletteDto rouletteDto)
        {
            // Get open bets
            List<BetDto> betDtos = rouletteDto.Bets.Where(x => x.State.IsOpen).ToList();

            Random random = new Random();
            // Between 0 y 36
            int number = random.Next(0, 36);

            for (int i = 0; i < betDtos.Count; i++)
            {
                double multiplier = 0;

                if (betDtos[i].IsColor)
                {
                    // If the generated number is 0, the bet by color is not applied, as it is the only number without color
                    if (!number.Equals(0))
                    {
                        // Verify that the color is red (Even number) and the generated number is also red
                        if ((betDtos[i].Number % 2).Equals(0) && (number % 2).Equals(0))
                        {
                            multiplier = 1.8;
                        }
                        // Verify that the color is black (odd number) and the generated number is also black
                        else if (!(betDtos[i].Number % 2).Equals(0) && !(number % 2).Equals(0))
                        {
                            multiplier = 1.8;
                        }
                    }
                } else
                {
                    multiplier = betDtos[i].Number.Equals(number) ? 5 : 0;
                }

                // Update prize value, roulette number and close bet
                await this._betService.UpdateAsync(betDtos[i].Id, new BetForUpdateDto() { Prize = (betDtos[i].Money * multiplier), RouletteNumber = number, StateId = new Guid("D38D2437-92B8-4C0C-BDE3-8C1029403F03") });
            }

            return await this.UpdateAsync(rouletteDto.Id, new RouletteForUpdateDto() { StateId = new Guid("D38D2437-92B8-4C0C-BDE3-8C1029403F03") });
        }

        public async Task<bool> ValidateAsync(Guid id)
        {
            if (!await this.ExistsAsync(id))
            {
                return false;
            }

            RouletteDto rouletteDto = this.Mapper.Map<RouletteDto>(await this.GetAsync(null, null, id));

            return rouletteDto.State.IsOpen;
        }

        private void SetPathRelatedEntities()
        {
            this.PathRelatedEntities = new List<string> { "State", "Bets.State" };
        }
    }
}
