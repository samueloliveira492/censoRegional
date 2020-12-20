
using CensoRegional.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CensoRegional.Api.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public void Post([FromBody] PersonCreateCommand request)
        {
            _mediator.Send(request);
        }

        [HttpDelete]
        public void Delete([FromBody] PersonDeleteCommand request)
        {
            _mediator.Send(request);
        }
    }
}
