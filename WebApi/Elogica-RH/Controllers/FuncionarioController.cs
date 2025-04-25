using Application.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elogica_RH.Controllers;

[Route("funcionarios")]
[ApiController]

public class FuncionarioController : MainController
{
    private readonly IFuncionarioService _service;

    public FuncionarioController(INotificador notificador, IFuncionarioService service) : base(notificador)
    {
        _service = service;
    }

    [HttpGet("{pagina}/{quantidade}")]
    public async Task<IActionResult> BuscarFuncionariosPaginadoAsync(int pagina, int quantidade)
    {
        try
        {
            var retorno = await _service.BuscarFuncionariosPaginadoAsync(pagina, quantidade);
            return CustomResponse(retorno);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> BuscarFuncionariosAsync()
    {
        try
        {
            var retorno = await _service.BuscarFuncionariosAsync();
            return CustomResponse(retorno);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarFuncionarioPorIdAsync(int id)
    {
        try
        {
            var retorno = await _service.BuscarFuncionarioPorIdAsync(id);
            return CustomResponse(retorno);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> AdicionarFuncionarioAsync([FromBody] FuncionarioDto funcionarioDto)
    {
        try
        {
            var retorno = await _service.AdicionarFuncionarioAsync(funcionarioDto);
            return CustomResponse(retorno);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarFuncionarioAsync(int id, [FromBody] FuncionarioDto funcionarioDto)
    {
        try
        {
            var retorno = await _service.AtualizarFuncionarioAsync(id, funcionarioDto);
            return CustomResponse(retorno);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }

    [HttpPut("desativar/{id}")]
    public async Task<IActionResult> DesativarFuncionarioAsync(int id)
    {
        try
        {
            var retorno = await _service.DesativarFuncionarioAsync(id);
            return CustomResponse(retorno);
        }
        catch (Exception e)
        {
            NotificarErro(e.Message);
            return CustomResponse();
        }
    }
}
