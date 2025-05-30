﻿using AutoMapper;
using Domain.Dtos;
using Domain.Models;
namespace Elogica_RH.Config;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Funcionario, FuncionarioDto>().ReverseMap();

        CreateMap<Menu, CriaMenuDto>().ReverseMap();
        CreateMap<Menu, AtualizaMenuDto>().ReverseMap();
        CreateMap<Menu, RespostaMenuDto>().ReverseMap();

        CreateMap<HorariosDto, Horarios>().ReverseMap();

        CreateMap<Setor, SetorDto>().ReverseMap();

        CreateMap<Cargos, CargosDto>().ReverseMap();

        CreateMap<Ferias, FeriasDto>().ReverseMap();
    }
}
