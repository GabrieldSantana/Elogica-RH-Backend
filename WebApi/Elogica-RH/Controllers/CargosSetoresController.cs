using System.ComponentModel.DataAnnotations;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Elogica_RH.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CargosSetoresController : MainController
{
    private readonly ICargosSetoresService _service;
    private readonly INotificador _notificador;

    public CargosSetoresController(ICargosSetoresService service, INotificador notificador) : base(notificador)
    {
        _service = service;
        _notificador = notificador;
    }

    [HttpGet]

    public async Task<IActionResult> BuscarCargosSetoresAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {

            }


            var resultado = await _service.BuscarCargosSetoresAsync();
            return CustomResponse(resultado);
        }
        catch (ValidationException ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }

    [HttpPost]

    public async Task<IActionResult> AdicionarCargosSetoresAsync([FromBody] CargosSetores cargosSetores)
    {
        try
        {

            if (!ModelState.IsValid)
            {

            }
            var resultado = await _service.AdicionarCargosSetoresAsync(cargosSetores);
            
                return CustomResponse(resultado);
            

        }
        catch (ValidationException ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }

    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> ExcluirCargosSetoresAsync(int id)
    {
        try
        {

            if (!ModelState.IsValid)
            {

            }

            var cargosSetores = await _service.ExcluirCargosSetoresAsync(id);
            return CustomResponse(ModelState);


        }
        catch (ValidationException ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    [HttpPut]
    public async Task<IActionResult> AtualizarCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {
            
            var retornoAtualizarCargosSetores = await _service.AtualizarCargosSetoresAsync(cargosSetores);
            return CustomResponse(retornoAtualizarCargosSetores);
        }
        catch (ValidationException ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }

    }

    [HttpGet("{quantidade}/{pagina}")]

    public async Task<IActionResult> BuscarCargosSetoresPaginado(int quantidade, int pagina)
    {
        try
        {
            var retornoBuscarCargosSetoresPaginado = await _service.BuscarCargosSetoresPaginadoAsync(quantidade, pagina);
            return CustomResponse(retornoBuscarCargosSetoresPaginado);
        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }

    }
}
