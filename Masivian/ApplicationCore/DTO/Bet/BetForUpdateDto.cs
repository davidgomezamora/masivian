using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.DTO.Bet
{
    public class BetForUpdateDto : BetForManipulationDto
    {
        public virtual int RouletteNumber { get; set; }
        [Range(0, 50000)]
        public virtual double? Prize { get; set; }
    }
}
