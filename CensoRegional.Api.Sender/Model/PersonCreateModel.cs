using CensoRegional.Util.Enums;
using System.Collections.Generic;

namespace CensoRegional.Api.Sender.Models
{
    public class PersonCreateModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public ColorType? Color { get; set; }
        public LevelEducationType? LevelEducation { get; set; }
        public IEnumerable<PersonCreateModel> Parents { get; set; }
        public IEnumerable<PersonCreateModel> Children { get; set; }
    }
}
