using AutoMapper;
using CensoRegional.Domain.Dto;
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
