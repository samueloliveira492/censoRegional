using CensoRegional.Api.Sender.Models;
using CensoRegional.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        // POST api/values
        [HttpPost]
        public void Post([FromBody] PersonCreateModel request)
        {
            _mediator.Send(new PersonCreatedCommand() { Person = null});
        }
    }
}
