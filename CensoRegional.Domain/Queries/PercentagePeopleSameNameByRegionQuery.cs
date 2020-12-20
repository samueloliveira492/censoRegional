using CensoRegional.Domain.Dtos;
using System.Collections.Generic;

namespace CensoRegional.Domain.Queries
{
    public class PercentagePeopleSameNameByRegionQuery: IQuery<IEnumerable<PercentagePeopleSameNameByRegionQueryDto>>
    {
        public string Region { get; set; }
    }
}
