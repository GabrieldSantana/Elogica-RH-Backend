using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Elogica_RH.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController: MainController
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService, INotificador notificador): base(notificador)
    {
        _menuService = menuService;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarMenuAsync([FromBody] CriaMenuDto menuDto)
    {
        try
        {
            var retorno = await _menuService.AdicionarMenuAsync(menuDto);
            return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarMenuAsync(int id, [FromBody] AtualizaMenuDto menuDto)
    {
        try
        {
            var retorno = await _menuService.AtualizarMenuAsync(id, menuDto);
            return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> BuscarMenuAsync()
    {
        try
        {
            var retorno = await _menuService.BuscarMenuAsync();
            return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("paginado")]
    public async Task<IActionResult> BuscarMenuPaginadoAsync([FromQuery] int pagina = 1, [FromQuery] int quantidade = 10)
    {
        try
        {
            var retorno = await _menuService.BuscarMenuPaginadoAsync(pagina, quantidade);
            return CustomResponse(retorno);
        }
        catch (Exception ex) 
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarMenuPorIdAsync(int id)
    {
        try
        {
            var retorno = await _menuService.BuscaMenuPorIdAsync(id);
            return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirSetoresAsync(int id)
    {
        try
        {
            var retorno = await _menuService.ExcluirMenuAsync(id);
            return CustomResponse(retorno);
        }
        catch(Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
}