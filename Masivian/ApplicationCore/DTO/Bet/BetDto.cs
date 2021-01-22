using ApplicationCore.DTO.Roulette;
using ApplicationCore.DTO.State;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO.Bet
{
    public class BetDto : IDto
    {
        public virtual Guid Id { get; set; }
        public virtual int Money { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual int? Prize { get; set; }
        public virtual Guid RouletteId { get; set; }
        public virtual Guid StateId { get; set; }
        public virtual RouletteDto Roulette { get; set; }
        public virtual StateDto State { get; set; }
    }
}
