using ApplicationCore.Services;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.ResourceParameters
{
    public class BetResourceParameters : ServiceParameters
    {
        public int? Money { get; set; }
        public int? Prize { get; set; }

        public BetResourceParameters()
        {
            this.OrderBy = "id";
        }
    }
}
