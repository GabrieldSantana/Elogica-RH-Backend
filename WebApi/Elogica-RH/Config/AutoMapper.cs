using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Elogica_RH.Config;

public class AutoMapper : Profile
{

    public AutoMapper()
    {
        CreateMap<SetorDto, Setor>();
        CreateMap<Setor, SetorDto>();
        CreateMap<Cargos, CargosDto>().ReverseMap();
    }
}
