using ApplicationCore.Services;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.ResourceParameters
{
    public class BetResourceParameters : ServiceParameters
    {
        public double? Money { get; set; }
        public double? Prize { get; set; }
        public bool? IsColor { get; set; }
        public int? RouletteNumber { get; set; }

        public BetResourceParameters()
        {
            this.OrderBy = "id";
        }
    }
}
