
using CensoRegional.Domain.Commands;
using CensoRegional.Domain.Messaging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CensoRegional.Api.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonSenderController : ControllerBase
    {
        private readonly IBusCommandPublisher _busCommandPublisher;
        public PersonSenderController(IBusCommandPublisher busCommandPublisher)
        {
            _busCommandPublisher = busCommandPublisher;
        }

        /// <summary>
        /// Create Person
        /// </summary>
        /// <param name="request">Person details.</param>
        [HttpPost]
        public IActionResult Post([FromBody] PersonCreateCommand request)
        {
            _busCommandPublisher.PublishCommandAsync(request);
            return Created("", null);
        }

        /// <summary>
        /// Delete Person by name and last name
        /// </summary>
        /// <param name="name">Name of the person.</param>
        /// <param name="lastName">Last name of the person.</param>
        [HttpDelete]
        public IActionResult Delete(string name, string lastName)
        {
            _busCommandPublisher.PublishCommandAsync(new PersonDeleteCommand { Name = name, LastName = lastName });
            return Ok();
        }
    }
}
