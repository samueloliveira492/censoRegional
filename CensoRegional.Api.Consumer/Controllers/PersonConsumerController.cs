using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Queries;
using CensoRegional.Util.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CensoRegional.Api.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonConsumerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonConsumerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get family tree by name and last name.
        /// </summary>
        /// <param name="name">Name of the root of the tree.</param>
        /// <param name="lastName">Last Name of the root of the tree.</param>
        /// <param name="level">Level.</param>
        /// <returns>Number of people who meet the filter.</returns>
        [Route("name/{name}/lastname/{lastName}/level/{level}")]
        [HttpGet]
        [ProducesResponseType(typeof(FamilyTreeByPersonQuery), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFamilyTreeByPerson(string name, string lastName, int level)
        {
            FamilyTreeByPersonQueryDto result = new FamilyTreeByPersonQueryDto();
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName))
                    return BadRequest();
                result = await _mediator.Send(new FamilyTreeByPersonQuery { Name = name, LastName = lastName, Level = level });
                
            } catch(Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok(result);

        }

        /// <summary>
        /// Get the number of people with the same name
        /// </summary>
        /// <param name="region">Region for find people that same name</param>
        /// <returns>List with number of people with the same name.</returns>
        [Route("region/{region}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PercentagePeopleSameNameByRegionQueryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PercentagePeopleSameNameByRegion(string region)
        {
            if (string.IsNullOrEmpty(region))
                return BadRequest();

            IEnumerable<PercentagePeopleSameNameByRegionQueryDto> result = await _mediator.Send(new PercentagePeopleSameNameByRegionQuery { Region = region });
            return Ok(result);

        }

        /// <summary>
        /// Get number of people whot meet the filter
        /// </summary>
        /// <param name="name">Name of the people.</param>
        /// <param name="lastName">Last name of the people.</param>
        /// <param name="color">Color of the people.</param>
        /// <param name="levelEducation">Level education of the people.</param>
        /// <returns>Number of people who meet the filter.</returns>
        [Route("quantity")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PercentagePeopleSameNameByRegionQueryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QuantityPeopleByManyFilters(string name, string lastName, ColorType color, LevelEducationType levelEducation)
        {
            QuantityPeopleByManyFiltersQueryDto result = await _mediator.Send(new QuantityPeopleByManyFiltersQuery { Name = name, LastName = lastName, ColorFilter = color, LevelEducationFilter = levelEducation });
            return Ok(result);

        }
    }
}
