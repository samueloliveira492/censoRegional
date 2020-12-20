using CensoRegional.Domain.Entity;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CensoRegional.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<Unit> CreatePerson(Person person);
        void CreateRelationship(Person parent, Person son, string relationship);
        Task<IEnumerable<Person>> GetAllPersonByRegion(string region);
    }
}
