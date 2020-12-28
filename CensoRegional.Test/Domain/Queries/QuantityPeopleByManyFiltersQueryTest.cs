using CensoRegional.Application.QueryHandlers;
using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Messaging;
using CensoRegional.Domain.Queries;
using CensoRegional.Domain.Repositories;
using CensoRegional.Util.Enums;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CensoRegional.Test.Domain.Queries
{
    public class QuantityPeopleByManyFiltersQueryTest
    {
        [Fact]
        public void GetQuantityPeopleByName_RepositoryReturnsNull()
        {

            var person = new Person { Name = "Nome" };
            var person2 = new Person { Name = "Nome" };
            var person3 = new Person { Name = "Nome" };

            var busPublisher = new Mock<IBusEventPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetPersonByConcatenationFilterCondition(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<ColorType?>(), It.IsAny<LevelEducationType?>())).ReturnsAsync((IEnumerable<Person>)null);

            QuantityPeopleByManyFiltersQuery query = new QuantityPeopleByManyFiltersQuery();
            QuantityPeopleByManyFiltersQueryHandler handler = new QuantityPeopleByManyFiltersQueryHandler(personRepository.Object);

            QuantityPeopleByManyFiltersQueryDto result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.True(result.Quantity == 0);

            personRepository.Verify(m => m.GetPersonByConcatenationFilterCondition(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<ColorType?>(), It.IsAny<LevelEducationType?>()), Times.Once);
        }

        [Fact]
        public void GetQuantityPeopleByName()
        {

            var person = new Person { Name = "Nome" };
            var person2 = new Person { Name = "Nome" };
            var person3 = new Person { Name = "Nome" };

            var busPublisher = new Mock<IBusEventPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetPersonByConcatenationFilterCondition(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<ColorType?>(), It.IsAny<LevelEducationType?>())).ReturnsAsync(new List<Person> { person, person2, person3 });
            var a = new List<Person>();

            QuantityPeopleByManyFiltersQuery query = new QuantityPeopleByManyFiltersQuery();
            QuantityPeopleByManyFiltersQueryHandler handler = new QuantityPeopleByManyFiltersQueryHandler(personRepository.Object);

            QuantityPeopleByManyFiltersQueryDto result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.True(result.Quantity == 3);

            personRepository.Verify(m => m.GetPersonByConcatenationFilterCondition(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<ColorType?>(), It.IsAny<LevelEducationType?>()), Times.Once);
        }
    }
}
