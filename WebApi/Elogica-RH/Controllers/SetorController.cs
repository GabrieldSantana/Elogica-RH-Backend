using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Elogica_RH.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetorController : MainController
{
    private readonly ISetorService _setorService;

    public SetorController(ISetorService setorService, INotificador notificador) : base(notificador)
    {
        _setorService = setorService;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarSetorAsync([FromBody] SetorDto setorDto)
    {
        try
        {
            var retorno = await _setorService.AdicionarSetoresAsync(setorDto);
            return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarSetorAsync(int id, [FromBody] SetorDto setorDto)
    {
        try
        {
            var retorno = await _setorService.AtualizarSetoresAsync(id, setorDto);
            return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet]
    public async Task<IActionResult> BuscarSetoresAsync()
    {
        try
        {
        var retorno = await _setorService.BuscarSetoresAsync();
        return CustomResponse(retorno);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("paginado")]
    public async Task<IActionResult> BuscarPaginadoAsync([FromQuery] int pagina = 1, [FromQuery] int quantidade = 10)
    {
        try
        {
            var retorno = await _setorService.BuscarSetoresPaginadoAsync(pagina, quantidade);
            return CustomResponse(retorno);
        }
        catch (Exception ex) 
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarSetoresPorIdAsync(int id)
    {
        try
        {
        var retorno = await _setorService.BuscarSetorPorIdAsync(id);
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
            var retorno = await _setorService.ExcluirSetoresAsync(id);
            return CustomResponse(retorno);
        }
        catch(Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
}
