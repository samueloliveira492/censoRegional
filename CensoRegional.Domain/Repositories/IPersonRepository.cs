﻿using CensoRegional.Domain.Entity;
using CensoRegional.Util.Enums;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CensoRegional.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<Unit> CreatePerson(Person person);
        Task<Unit> DeletePerson(Person person);
        void CreateRelationship(Person parent, Person son, string relationship);
        Task<IEnumerable<Person>> GetAllPersonByRegion(string region);
        Task<IEnumerable<Person>> GetChildrenByNameAndLastName(string name, string lastName);
        Task<IEnumerable<Person>> GetByNameAndLastName(string name, string lastName);
        Task<IEnumerable<Person>> GetPersonByConcatenationFilterCondition(string name, string lastName, ColorType? color, LevelEducationType? levelEducation);
    }
}
