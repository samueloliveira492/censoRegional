using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Queries;
using CensoRegional.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Application.QueryHandlers
{
    public class QuantityPeopleByManyFiltersQueryHandler : IQueryHandler<QuantityPeopleByManyFiltersQuery, QuantityPeopleByManyFiltersQueryDto>
    {
        private readonly IPersonRepository _personRepository;
        public QuantityPeopleByManyFiltersQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<QuantityPeopleByManyFiltersQueryDto> Handle(QuantityPeopleByManyFiltersQuery request, CancellationToken cancellationToken)
        {
            QuantityPeopleByManyFiltersQueryDto resultado = new QuantityPeopleByManyFiltersQueryDto();
            IEnumerable<Person> people = await _personRepository.GetPersonByConcatenationFilterCondition(request.Name, request.LastName, request.ColorFilter, request.LevelEducationFilter);
            resultado.Quantity = people.Count();
            return resultado;
        }
    }
}
