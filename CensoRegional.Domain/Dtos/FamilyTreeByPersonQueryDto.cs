using System.Collections.Generic;

namespace CensoRegional.Domain.Dtos
{
    public class FamilyTreeByPersonQueryDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public IEnumerable<FamilyTreeByPersonQueryDto> Children{ get; set; }
    }
}
