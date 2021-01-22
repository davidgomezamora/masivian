using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO.Roulette
{
    public class RouletteForAdditionDto : RouletteForManipulationDto
    {
        public RouletteForAdditionDto()
        {
            this.StateId = new Guid("D38D2437-92B8-4C0C-BDE3-8C1029403F03");
        }
    }
}
