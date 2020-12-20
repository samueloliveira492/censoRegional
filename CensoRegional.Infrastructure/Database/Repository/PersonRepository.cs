﻿using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Repositories;
using CensoRegional.Util.Constantes;
using CensoRegional.Util.Enums;
using MediatR;
using Neo4jClient;
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
            .Create($"(n:{ typeof(Person) }) SET n = $newNode")
            .WithParam("newNode", person)
            .ExecuteWithoutResultsAsync();
            return Unit.Value;
        }

        public async Task<Unit> DeletePerson(Person person)
        {
            await _graphClient.Cypher
            .Match($"(a:{ typeof(Person) })")
            .Where((Person a) => a.Name.Equals(person.Name))
            .AndWhere((Person a) => a.Name.Equals(person.LastName))
            .Delete("a").ExecuteWithoutResultsAsync();
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

        public async Task<IEnumerable<Person>> GetChildrenByNameAndLastName(string name, string lastName)
        {
            return await _graphClient.Cypher
            .Match($"(a:{ typeof(Person) })-[r:" + Relacionamentos.PARENT + $"]->(b:{ typeof(Person) })")
            .Where((Person a) => a.Name.Equals(name))
            .AndWhere((Person a) => a.LastName.Equals(lastName))
            .Return<Person>("b").ResultsAsync;
        }

        public async Task<IEnumerable<Person>> GetPersonByConcatenationFilterCondition(string name, string lastName, ColorType? color, LevelEducationType? levelEducation)
        {
            return await _graphClient.Cypher
            .Match($"(a:{ typeof(Person) })")
            .WhereIf(!string.IsNullOrEmpty(name), (Person a) => a.Name.Equals(name))
            .AndWhereIf(!string.IsNullOrEmpty(lastName), (Person a) => a.LastName.Equals(lastName))
            .AndWhereIf(color.HasValue, (Person a) => a.Color == color)
            .AndWhereIf(levelEducation.HasValue, (Person a) => a.LevelEducation == levelEducation)
            .Return<Person>("a").ResultsAsync;
        }
    }
}
