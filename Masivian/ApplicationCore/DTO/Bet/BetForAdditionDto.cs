using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.DTO.Bet
{
    public class BetForAdditionDto : BetForManipulationDto
    {
        [Range(1, 10000)]
        public virtual double Money { get; set; }
        [Range(0, 36)]
        public virtual int Number { get; set; }
        public virtual bool IsColor { get; set; }
        public virtual Guid RouletteId { get; set; }
        [JsonIgnore]
        public virtual Guid UserId { get; set; }
        [JsonIgnore]
        public override Guid StateId { get => base.StateId; set => base.StateId = value; }

        public BetForAdditionDto()
        {
            this.StateId = new Guid("42290071-BD11-4089-9483-45612311FF52");
        }
    }
}
