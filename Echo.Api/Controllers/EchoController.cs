using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Echo.Core.Shared.Result;
using Echo.Application.Echos.Commands.AddEcho;
using Swashbuckle.AspNetCore.Annotations;

namespace Echo.Api.Controllers
{
    [ApiController]
    [Route(BaseApiPath + "/Echos")]
    public class EchoController : BaseController
    {
        /// <summary>
        /// Controllers should never use auth. Please refer to the documentation provided. 
        /// </summary>
        /// <param name="userId"> The userId is always provided by the API customers</param>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Hello world";
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create new Echo", Description = "Create new Echo")]
        public async Task<Result<EchoViewModel>> Create([FromBody] CreateEchoRequestDto createEchoRequestDto,
        CancellationToken cancellationToken)
        {
            var command= Mapper.Map<CreateEchoRequestDto, CreateEchoCommand>(createEchoRequestDto);

            var result = await Mediator.Send(command, cancellationToken);

            return Result<EchoViewModel>.Success(result);
        }

    }

}

