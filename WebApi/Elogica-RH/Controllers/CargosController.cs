using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Elogica_RH.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CargosController : MainController
{
    private readonly ICargosServices _service;

    public CargosController(INotificador notificador, ICargosServices service) : base(notificador)
    {
        _service = service;
    }

    [HttpGet("pagina/{pagina}/{quantidade}")]
    public async Task<IActionResult> BuscarCargosPaginadoAsync(int pagina, int quantidade)
    {
        try
        {
            var resultado = await _service.BuscarCargosPaginadoAsync(pagina, quantidade);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarCargosPorIdAsync(int id)
    {
        try
        {
            var resultado = await _service.BuscarCargosPorIdAsync(id);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpGet]
    public async Task<IActionResult> BuscarCargosAsync()
    {
        try
        {
            var resultado = await _service.BuscarCargosAsync();
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarCargosAsync([FromBody] CargosDto cargosDto)
    {
        try
        {
            var resultado = await _service.AdicionarCargosAsync(cargosDto);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarCargosAsync(int id, [FromBody] AtualizarCargosDto cargosDto)
    {
        try
        {
            var resultado = await _service.AtualizarCargosAsync(id, cargosDto);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirCargosAsync(int id)
    {
        try
        {
            var resultado = await _service.ExcluirCargosAsync(id);
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {
            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
}