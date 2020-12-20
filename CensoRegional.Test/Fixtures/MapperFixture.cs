using AutoMapper;
using CensoRegional.Domain.Mapping;

namespace CensoRegional.Test.Fixtures
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(op => {
                op.AddProfile(new PersonMap());
            });
            Mapper = config.CreateMapper();
        }
    }
}
