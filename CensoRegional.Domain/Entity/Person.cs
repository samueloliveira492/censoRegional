using CensoRegional.Util.Enums;

namespace CensoRegional.Domain.Entity
{
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public ColorType? Color { get; set; }
        public LevelEducationType? LevelEducation { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(LastName);
        }
    }
}
