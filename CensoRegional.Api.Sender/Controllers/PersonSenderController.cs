
using CensoRegional.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CensoRegional.Api.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonSenderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonSenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonCreateCommand request)
        {
            _mediator.Send(request);
            return Created("", null);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] PersonDeleteCommand request)
        {
            _mediator.Send(request);
            return Ok();
        }
    }
}
