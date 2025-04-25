using AutoMapper;
using Domain.Dtos;

namespace Application
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Ferias, FeriasDto>().ReverseMap();

            
        }
    }
}
