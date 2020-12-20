using CensoRegional.Domain.Dtos;

namespace CensoRegional.Domain.Queries
{
    public class FamilyTreeByPersonQuery : IQuery<FamilyTreeByPersonQueryDto>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
    }
}
