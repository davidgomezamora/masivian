using ApplicationCore.DTO.Bet;
using ApplicationCore.ResourceParameters;
using ApplicationCore.Services.BetService;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Masivian.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("0.5", Deprecated = true)]
    [Route("api/[controller]")]
    public class BetController : BaseController
    {
        private readonly IBetService _rouletteService;

        public BetController(IBetService rouletteService)
        {
            this._rouletteService = rouletteService ??
                throw new ArgumentNullException(nameof(rouletteService));
        }

        [HttpGet(Name = "GetBetsPagedListAsync")]
        public async Task<ActionResult<PagedList<ExpandoObject>>> GetPagedListAsync([FromQuery] BetResourceParameters resourceParameters)
        {
            if (!this._rouletteService.ValidateOrderByString(resourceParameters.OrderBy) || !this._rouletteService.ValidateFields(resourceParameters.Fields))
            {
                return BadRequest();
            }

            PagedList<ExpandoObject> pagedList = await this._rouletteService.GetAsync(resourceParameters);

            if (!pagedList.Results.Any())
            {
                return NoContent();
            }

            if (pagedList.HasPrevious)
            {
                pagedList.PreviousPageLink = CreateResourceUri(resourceParameters, ResourceUriTypeEnum.PreviousPage, "GetBetsPagedListAsync");
            }

            if (pagedList.HasNext)
            {
                pagedList.NextPageLink = CreateResourceUri(resourceParameters, ResourceUriTypeEnum.NextPage, "GetBetsPagedListAsync");
            }

            return Ok(pagedList);
        }

        [HttpGet("list", Name = "GetBetsAsync")]
        public async Task<ActionResult<List<ExpandoObject>>> GetListAsync([FromQuery] BetResourceParameters resourceParameters)
        {
            if (!this._rouletteService.ValidateOrderByString(resourceParameters.OrderBy) || !this._rouletteService.ValidateFields(resourceParameters.Fields))
            {
                return BadRequest();
            }

            List<ExpandoObject> expandoObjects = await this._rouletteService.GetListAsync(resourceParameters);

            if (!expandoObjects.Any())
            {
                return NoContent();
            }

            return Ok(expandoObjects);
        }

        [HttpGet("{id}", Name = "GetBetAsync")]
        public async Task<ActionResult<ExpandoObject>> GetAsync(Guid id, string fields)
        {
            if (!this._rouletteService.ValidateFields(fields))
            {
                return BadRequest();
            }

            ExpandoObject expandoObject = await this._rouletteService.GetAsync(fields, null, id);

            if (expandoObject is null)
            {
                return NotFound();
            }

            List<LinkDto> linkDtos = new List<LinkDto>();
            linkDtos.Add(new LinkDto(Url.Link("GetBetAsync", new { id }), "partially_update", "PATCH"));

            IDictionary<string, object> linkedResource = expandoObject;

            linkedResource.Add("links", linkDtos);

            return Ok((ExpandoObject)linkedResource);
        }

        [HttpPost(Name = "AddBetAsync")]
        public async Task<ActionResult<BetDto>> AddAsync()
        {
            BetDto dto = await this._rouletteService.AddAsync(new BetForAdditionDto());

            if (dto is null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetBetAsync", new { Id = dto.GetId() }, dto);
        }
    }
}
