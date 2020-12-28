using CensoRegional.Domain.Dtos;
using CensoRegional.Util.Enums;

namespace CensoRegional.Domain.Queries
{
    public class QuantityPeopleByManyFiltersQuery : IQuery<QuantityPeopleByManyFiltersQueryDto>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public ColorType? ColorFilter { get; set; }
        public LevelEducationType? LevelEducationFilter { get; set; }
    }
}
