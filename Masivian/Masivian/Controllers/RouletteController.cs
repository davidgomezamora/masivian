using ApplicationCore.DTO.Roulette;
using ApplicationCore.ResourceParameters;
using ApplicationCore.Services.RouletteService;
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
    public class RouletteController : BaseController
    {
        private readonly IRouletteService _rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            this._rouletteService = rouletteService ??
                throw new ArgumentNullException(nameof(rouletteService));
        }

        [HttpGet(Name = "GetRoulettesPagedListAsync")]
        public async Task<ActionResult<PagedList<ExpandoObject>>> GetPagedListAsync([FromQuery] RouletteResourceParameters resourceParameters)
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
                pagedList.PreviousPageLink = CreateResourceUri(resourceParameters, ResourceUriTypeEnum.PreviousPage, "GetRoulettesPagedListAsync");
            }

            if (pagedList.HasNext)
            {
                pagedList.NextPageLink = CreateResourceUri(resourceParameters, ResourceUriTypeEnum.NextPage, "GetRoulettesPagedListAsync");
            }

            return Ok(pagedList);
        }

        [HttpGet("list", Name = "GetRoulettesAsync")]
        public async Task<ActionResult<List<ExpandoObject>>> GetListAsync([FromQuery] RouletteResourceParameters resourceParameters)
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

        [HttpGet("{id}", Name = "GetRouletteAsync")]
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
            linkDtos.Add(new LinkDto(Url.Link("GetRouletteAsync", new { id }), "partially_update", "PATCH"));

            IDictionary<string, object> linkedResource = expandoObject;

            linkedResource.Add("links", linkDtos);

            return Ok((ExpandoObject)linkedResource);
        }

        [HttpPost(Name = "AddRouletteAsync")]
        public async Task<ActionResult<RouletteDto>> AddAsync()
        {
            RouletteDto dto = await this._rouletteService.AddAsync(new RouletteForAdditionDto());

            if (dto is null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetRouletteAsync", new { Id = dto.GetId() }, dto);
        }

        [HttpPatch("{id}/close", Name = "PartiallyUpdateRouletteAsync")]
        public async Task<ActionResult> PartiallyUpdateAsync(Guid Id)
        {
            if (await this._rouletteService.ExistsAsync(Id))
            {
                if (await this._rouletteService.Close(Id))
                {
                    return NoContent();
                }

                // 304 (Not Modified)
                return StatusCode(304);
            }

            return NotFound();
        }
    }
}
