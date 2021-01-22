using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO.Roulette
{
    public class RouletteForAdditionDto : RouletteForManipulationDto
    {
        public RouletteForAdditionDto()
        {
            this.StateId = new Guid("42290071-BD11-4089-9483-45612311FF52");
        }
    }
}
