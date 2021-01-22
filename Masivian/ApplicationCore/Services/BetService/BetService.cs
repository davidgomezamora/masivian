using ApplicationCore.DTO.Bet;
using ApplicationCore.Helpers;
using ApplicationCore.ResourceParameters;
using ApplicationCore.Services.RouletteService;
using AutoMapper;
using Common;
using Infraestructure.Entities;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.BetService
{
    public class BetService : BaseService<Bet, BetDto, BetForAdditionDto, BetForUpdateDto, BetForSortingDto, BetResourceParameters>, IBetService
    {
        public BetService(IRepository<Bet> repository, IMapper mapper) : base(repository, mapper) {
            this.PathRelatedEntities = new List<string> { "State", "Roulette" };
        }

        public override void BuildSearchQueryFilter(BetResourceParameters parameters, out QueryParameters<Bet> queryParameters)
        {
            queryParameters = new QueryParameters<Bet>();

            if (!(parameters.Money is null))
            {
                queryParameters.WhereList.Add(x => x.Money.Equals(parameters.Money));
            }

            if (!(parameters.Prize is null))
            {
                queryParameters.WhereList.Add(x => x.Prize.Equals(parameters.Prize));
            }

            if (!(parameters.IsColor is null))
            {
                queryParameters.WhereList.Add(x => x.IsColor.Equals(parameters.IsColor));
            }

            if (!(parameters.RouletteNumber is null))
            {
                queryParameters.WhereList.Add(x => x.RouletteNumber.Equals(parameters.RouletteNumber));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                parameters.SearchQuery = parameters.SearchQuery.Trim();

                queryParameters.WhereList.Add(x => x.Money.ToString().ToLower().Contains(parameters.SearchQuery.ToLower()));
            }
        }
    }
}
