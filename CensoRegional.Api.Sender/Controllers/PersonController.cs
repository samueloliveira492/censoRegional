
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
        public void Post([FromBody] PersonCreatedCommand request)
        {
            _mediator.Send(request);
        }
    }
}
