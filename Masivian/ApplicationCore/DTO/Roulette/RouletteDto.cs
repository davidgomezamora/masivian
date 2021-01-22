using ApplicationCore.DTO.Bet;
using ApplicationCore.DTO.State;
using ApplicationCore.Services;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO.Roulette
{
    public class RouletteDto : IDto
    {
        public virtual Guid Id { get; set; }
        public virtual Guid StateId { get; set; }
        public virtual StateDto State { get; set; }
        public virtual List<BetDto> Bets { get; set; }
        public object GetId()
        {
            return this.Id;
        }
    }
}
