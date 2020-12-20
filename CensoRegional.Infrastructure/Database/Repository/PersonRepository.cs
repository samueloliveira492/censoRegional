using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Repositories;
using MediatR;
using Neo4jClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CensoRegional.Infrastructure.Database.Repository
{
    public class PersonRepository: IPersonRepository
    {
        public IGraphClient _graphClient;
        public PersonRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<Unit> CreatePerson(Person person)
        {
            await _graphClient.Cypher
            .Create($"(n:{ typeof(Person) }) SET n = $newNode")
            .WithParam("newNode", person)
            .ExecuteWithoutResultsAsync();
            return Unit.Value;
        }

        public void CreateRelationship(Person parent, Person son, string relationship)
        {
            _graphClient.Cypher
            .Match($"(a:{ typeof(Person) })", $"(b:{ typeof(Person) })")
            .Where((Person a) => a.Name == parent.Name && a.LastName == parent.LastName)
            .AndWhere((Person b) => b.Name == son.Name && b.LastName == son.LastName)
            .Create("(a)-[r:"+relationship+"]->(b)")
            .ExecuteWithoutResultsAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersonByRegion(string region)
        {
            return await _graphClient.Cypher
            .Match($"(a:{ typeof(Person) })")
            .Where((Person a) => a.Region.Equals(region))
            .Return<Person>("a").ResultsAsync;
        }
    }
}
