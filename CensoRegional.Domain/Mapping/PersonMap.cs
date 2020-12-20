using AutoMapper;
using CensoRegional.Domain.Dtos;
using CensoRegional.Domain.Entity;

namespace CensoRegional.Domain.Mapping
{
    public class PersonMap : Profile
    {
        public PersonMap()
        {
            CreateMap<PersonCreateDto, Person>().ReverseMap();
        }
    }
}
