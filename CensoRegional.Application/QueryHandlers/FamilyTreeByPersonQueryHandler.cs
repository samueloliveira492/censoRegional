using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Queries;
using CensoRegional.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CensoRegional.Application.QueryHandlers
{
    public class FamilyTreeByPersonQueryHandler : IQueryHandler<FamilyTreeByPersonQuery, FamilyTreeByPersonQueryDto>
    {
        private readonly IPersonRepository _personRepository;
        public FamilyTreeByPersonQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<FamilyTreeByPersonQueryDto> Handle(FamilyTreeByPersonQuery request, CancellationToken cancellationToken)
        {
            FamilyTreeByPersonQueryDto resultado = null;
            var personCleaning = _personRepository.GetByNameAndLastName(request.Name, request.LastName).Result;
            if (personCleaning != null && personCleaning.Any())
            {
                resultado = new FamilyTreeByPersonQueryDto();
                resultado.Name = request.Name;
                resultado.LastName = request.LastName;

                resultado.Children = new List<FamilyTreeByPersonQueryDto>();
                if (request.Level > 0)
                    resultado.Children = await GetTreeFamily(request.Name, request.LastName, request.Level);

            }
            return resultado;
            
        }

        private async Task<IEnumerable<FamilyTreeByPersonQueryDto>> GetTreeFamily(string name, string lastName, int level)
        {
            if (level == 0)
                return null;
            else
            {
                IEnumerable<Person> childrenCleaning = _personRepository.GetChildrenByNameAndLastName(name, lastName).Result;
                if (childrenCleaning.Any())
                {
                    IEnumerable<FamilyTreeByPersonQueryDto> children = childrenCleaning.Select(c => new FamilyTreeByPersonQueryDto { Name = c.Name, LastName = c.LastName }).ToList();
                    foreach (var child in children)
                    {
                        child.Children = await GetTreeFamily(child.Name, child.LastName, level - 1);
                    }
                    return children;
                }
                return null;
            }
        }
    }
}
