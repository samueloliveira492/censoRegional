using CensoRegional.Util.Enums;
using System.Collections.Generic;

namespace CensoRegional.Api.Sender.Models
{
    public class PersonCreateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public ColorType? Color { get; set; }
        public LevelEducationType? LevelEducation { get; set; }
        public IEnumerable<PersonCreateDto> Parents { get; set; }
        public IEnumerable<PersonCreateDto> Children { get; set; }
    }
}
