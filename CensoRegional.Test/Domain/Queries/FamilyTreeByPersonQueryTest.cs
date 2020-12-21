using CensoRegional.Application.QueryHandlers;
using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Entity;
using CensoRegional.Domain.Messaging;
using CensoRegional.Domain.Queries;
using CensoRegional.Domain.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CensoRegional.Test.Domain.Queries
{
    public class FamilyTreeByPersonQueryTest
    {
        [Fact]
        public void ObtemArvore_UsuarioNaoExistente()
        {
            var person = new Person { Name = "Nome", LastName = "Sobrenome" };
            var busPublisher = new Mock<IBusPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetByNameAndLastName(person.Name, person.LastName)).ReturnsAsync(new List<Person>());
            var a = new List<Person>();
            FamilyTreeByPersonQuery query = new FamilyTreeByPersonQuery();
            FamilyTreeByPersonQueryHandler handler = new FamilyTreeByPersonQueryHandler(personRepository.Object);

            FamilyTreeByPersonQueryDto result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.Null(result);

            personRepository.Verify(m => m.GetByNameAndLastName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ObtemArvore_UsuarioExistente_SemFilhos()
        {
            var person = new Person { Name = "Nome", LastName = "Sobrenome" };
            var busPublisher = new Mock<IBusPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetByNameAndLastName(person.Name, person.LastName)).ReturnsAsync(new List<Person>() { person });
            personRepository.Setup(repo => repo.GetChildrenByNameAndLastName(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<Person>());
            var a = new List<Person>();
            FamilyTreeByPersonQuery query = new FamilyTreeByPersonQuery() {Name = person.Name, LastName = person.LastName, Level = 2 };

            FamilyTreeByPersonQueryHandler handler = new FamilyTreeByPersonQueryHandler(personRepository.Object);

            FamilyTreeByPersonQueryDto result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.Equal(result.Name, person.Name);
            Assert.Equal(result.LastName, person.LastName);
            Assert.Null(result.Children);

            personRepository.Verify(m => m.GetByNameAndLastName(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            personRepository.Verify(m => m.GetChildrenByNameAndLastName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ObtemArvore_UsuarioExistente_ComFilho()
        {
            var person = new Person { Name = "Nome", LastName = "Sobrenome" };
            var personChild = new Person { Name = "NomeFilho", LastName = "SobrenomeFilho" };
            var busPublisher = new Mock<IBusPublisher>();
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(repo => repo.GetByNameAndLastName(person.Name, person.LastName)).ReturnsAsync(new List<Person>() { person });
            personRepository.Setup(repo => repo.GetChildrenByNameAndLastName(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<Person>() { personChild });
            var a = new List<Person>();
            FamilyTreeByPersonQuery query = new FamilyTreeByPersonQuery() { Name = person.Name, LastName = person.LastName, Level = 2 };

            FamilyTreeByPersonQueryHandler handler = new FamilyTreeByPersonQueryHandler(personRepository.Object);

            FamilyTreeByPersonQueryDto result = handler.Handle(query, new System.Threading.CancellationToken()).Result;

            Assert.NotNull(result);
            Assert.Equal(result.Name, person.Name);
            Assert.Equal(result.LastName, person.LastName);
            Assert.True(result.Children.Any());

            personRepository.Verify(m => m.GetByNameAndLastName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            personRepository.Verify(m => m.GetChildrenByNameAndLastName(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}