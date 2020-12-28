using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Repositories;
using CensoRegional.Util.Constantes;
using CensoRegional.Util.Enums;
using MediatR;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CensoRegional.Infrastructure.Database.Repository
{
    public class PersonRepository: IPersonRepository
    {
        private readonly IGraphClient _graphClient;
        public PersonRepository(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<Unit> CreatePerson(Person person)
        {
            await _graphClient.Cypher
            .Create($"(n:Person) SET n = $newNode")
            .WithParam("newNode", person)
            .ExecuteWithoutResultsAsync();
            
            return Unit.Value;
        }

        public async Task<Unit> DeletePerson(string name, string lastName)
        {
            await _graphClient.Cypher
            .Match($"(a:Person)")
            .Where((Person a) => a.Name == name)
            .AndWhere((Person a) => a.LastName == lastName)
            .Delete("a").ExecuteWithoutResultsAsync();
            return Unit.Value;
        }

        public void CreateRelationship(Person parent, Person son, string relationship)
        {
            _graphClient.Cypher
            .Match($"(a:Person)", $"(b:Person)")
            .Where((Person a) => a.Name == parent.Name && a.LastName == parent.LastName)
            .AndWhere((Person b) => b.Name == son.Name && b.LastName == son.LastName)
            .Create("(a)-[r:"+relationship+"]->(b)")
            .ExecuteWithoutResultsAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersonByRegion(string region)
        {
            return await _graphClient.Cypher
            .Match("(a:Person)")
            .Where((Person a) => a.Region == region)
            .Return<Person>("a").ResultsAsync;
        }

        public async Task<IEnumerable<Person>> GetChildrenByNameAndLastName(string name, string lastName)
        {
            return await _graphClient.Cypher
            .Match($"(a:Person)-[r:" + Relacionamentos.PARENT + $"]->(b:Person)")
            .Where((Person a) => a.Name == name)
            .AndWhere((Person a) => a.LastName == lastName)
            .Return<Person>("b").ResultsAsync;
        }

        public async Task<IEnumerable<Person>> GetByNameAndLastName(string name, string lastName)
        {
            return await _graphClient.Cypher
            .Match($"(a:Person)")
            .Where((Person a) => a.Name == name)
            .AndWhere((Person a) => a.LastName == lastName)
            .Return<Person>("a").ResultsAsync;
        }

        public async Task<IEnumerable<Person>> GetPersonByConcatenationFilterCondition(string name, string lastName, string region, ColorType? color, LevelEducationType? levelEducation)
        {
             return await _graphClient.Cypher
            .Match($"(a:Person)")
            .WhereIf(!string.IsNullOrEmpty(name), (Person a) => a.Name == name)
            .WhereIf(string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName), (Person a) => a.LastName == lastName)
            .WhereIf(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(region), (Person a) => a.Region == region)
            .WhereIf(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(region) && color.HasValue && color.Value > 0, (Person a) => a.Color == color)
            .WhereIf(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(region) && (!color.HasValue || color.Value == 0) && levelEducation.HasValue && levelEducation.Value > 0, (Person a) => a.LevelEducation == levelEducation)
            .AndWhereIf(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName), (Person a) => a.LastName == lastName)
            .AndWhereIf(!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(region), (Person a) => a.Region == region)
            .AndWhereIf((!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(lastName) || !string.IsNullOrEmpty(region)) && color.HasValue && color.Value > 0, (Person a) => a.Color == color)
            .AndWhereIf((!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(lastName) || !string.IsNullOrEmpty(region) || (color.HasValue && color.Value > 0)) && levelEducation.HasValue && levelEducation.Value > 0, (Person a) => a.LevelEducation == levelEducation)
            .Return<Person>("a").ResultsAsync;
        }
    }
}
