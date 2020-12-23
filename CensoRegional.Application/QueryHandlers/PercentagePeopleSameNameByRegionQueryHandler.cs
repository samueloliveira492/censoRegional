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
    public class PercentagePeopleSameNameByRegionQueryHandler : IQueryHandler<PercentagePeopleSameNameByRegionQuery, IEnumerable<PercentagePeopleSameNameByRegionQueryDto>>
    {
        private readonly IPersonRepository _personRepository;
        public PercentagePeopleSameNameByRegionQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PercentagePeopleSameNameByRegionQueryDto>> Handle(PercentagePeopleSameNameByRegionQuery request, CancellationToken cancellationToken)
        {
            List<PercentagePeopleSameNameByRegionQueryDto> resultado = new List<PercentagePeopleSameNameByRegionQueryDto>();
            IEnumerable<Person> people = await _personRepository.GetAllPersonByRegion(request.Region);
            
            if (people.Any())
            {
                double total = people.Count();
                resultado.AddRange(people.GroupBy(p => p.Name).Select(p1 =>
                new PercentagePeopleSameNameByRegionQueryDto { Name = p1.First().Name, Percentage = (p1.Count() / total) * 100 }
                ).ToList());
            }
            

            return resultado;
        }
    }
}
