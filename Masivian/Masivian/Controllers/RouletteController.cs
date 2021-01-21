using ApplicationCore.ResourceParameters;
using ApplicationCore.Services.RouletteService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masivian.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("0.5", Deprecated = true)]
    [Route("api/[controller]")]
    public class RouletteController : BaseController
    {
        private readonly IRouletteService _rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            this._rouletteService = rouletteService ??
                throw new ArgumentNullException(nameof(rouletteService));
        }
    }
}
