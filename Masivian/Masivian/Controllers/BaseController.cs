using ApplicationCore.Services;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masivian.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public string UserIdClaim { get; set; }

        public string CreateResourceUri<TResourceParameters>(TResourceParameters resourceParameters, ResourceUriTypeEnum resourceUriType, string methodName) where TResourceParameters : ServiceParameters
        {
            switch (resourceUriType)
            {
                case ResourceUriTypeEnum.PreviousPage:
                    resourceParameters.PageNumber--;

                    return Url.Link(methodName, resourceParameters);

                case ResourceUriTypeEnum.NextPage:
                    resourceParameters.PageNumber++;

                    return Url.Link(methodName, resourceParameters);

                default:
                    return Url.Link(methodName, resourceParameters);
            }
        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
