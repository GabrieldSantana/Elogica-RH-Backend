using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace Elogica_RH.Config;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<HorariosDto, Horarios>().ReverseMap();
        
        CreateMap<Setor, SetorDto>().ReverseMap();
        
        CreateMap<Cargos, CargosDto>().ReverseMap();
    }
}
