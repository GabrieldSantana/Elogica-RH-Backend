using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repository;
    private readonly IMapper _mapper;

    public MenuService(IMenuRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RespostaMenuDto>> BuscarMenuAsync()
    {
        try
        {
            var listaMenu =  await _repository.BuscarMenuAsync();
            var result = _mapper.Map<List<RespostaMenuDto>>(listaMenu);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<RespostaMenuDto> BuscaMenuPorIdAsync(int id)
    {
        try
        {
            Menu menu = await _repository.BuscaMenuPorIdAsync(id);
            var result = _mapper.Map<RespostaMenuDto>(menu);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> AdicionarMenuAsync(CriaMenuDto menu)
    {
        try
        {
            Menu menuObj = _mapper.Map<Menu>(menu);
            var result = await _repository.AdicionarMenuAsync(menuObj);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> AtualizarMenuAsync(AtualizaMenuDto menu)
    {
        Menu menuObj = _mapper.Map<Menu>(menu);
        var result = await _repository.AtualizarMenuAsync(menuObj);
        return result;
    }

    public Task<RetornoPaginado<Menu>> BuscarMenuPaginadoAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExcluirMenuAsync(int id)
    {
        try
        {
            var result = await _repository.ExcluirMenuAsync(id);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}