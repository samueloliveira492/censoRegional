using CensoRegional.Application.QueryHandlers;
using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Messaging;
using CensoRegional.Domain.Queries;
using CensoRegional.Domain.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CensoRegional.Test.Domain.Queries
{
    public class PercentagePeopleSameNameByRegionQueryTest
    {
        [Fact]
        public void GetQuantityPeopleWithSameName_RepositoryReturnsNone()
        {

            var busPublisher = new Mock<IBusEventPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetAllPersonByRegion(It.IsAny<string>())).ReturnsAsync(new List<Person> { });

            PercentagePeopleSameNameByRegionQuery query = new PercentagePeopleSameNameByRegionQuery();
            PercentagePeopleSameNameByRegionQueryHandler handler = new PercentagePeopleSameNameByRegionQueryHandler(personRepository.Object);

            IEnumerable<PercentagePeopleSameNameByRegionQueryDto> result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.True(!result.Any());

            personRepository.Verify(m => m.GetAllPersonByRegion(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetQuantityPeopleWithSameName_NoSameName()
        {
            var person = new Person { Name = "Nome1" };
            var person2 = new Person { Name = "Nome2" };
            var person3 = new Person { Name = "Nome3" };
            var person4 = new Person { Name = "Nome4" };

            var busPublisher = new Mock<IBusEventPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetAllPersonByRegion(It.IsAny<string>())).ReturnsAsync(new List<Person> { person, person2, person3, person4 });

            PercentagePeopleSameNameByRegionQuery query = new PercentagePeopleSameNameByRegionQuery();
            PercentagePeopleSameNameByRegionQueryHandler handler = new PercentagePeopleSameNameByRegionQueryHandler(personRepository.Object);

            IEnumerable<PercentagePeopleSameNameByRegionQueryDto> result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.True(!result.Any());

            personRepository.Verify(m => m.GetAllPersonByRegion(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetQuantityPeopleWithSameName()
        {
            var person = new Person { Name = "Nome" };
            var person2 = new Person { Name = "Nome" };
            var person3 = new Person { Name = "Nome" };
            var person4 = new Person { Name = "Nome2" };

            var busPublisher = new Mock<IBusEventPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetAllPersonByRegion(It.IsAny<string>())).ReturnsAsync(new List<Person> { person, person2, person3, person4});

            PercentagePeopleSameNameByRegionQuery query = new PercentagePeopleSameNameByRegionQuery();
            PercentagePeopleSameNameByRegionQueryHandler handler = new PercentagePeopleSameNameByRegionQueryHandler(personRepository.Object);

            IEnumerable<PercentagePeopleSameNameByRegionQueryDto> result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.Equal("Nome", result.FirstOrDefault().Name);
            Assert.True(result.FirstOrDefault().Percentage == 75);

            personRepository.Verify(m => m.GetAllPersonByRegion(It.IsAny<string>()), Times.Once);
        }
    }
}