using Application.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elogica_RH.Controllers;

[Route("cargossetores")]
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

    #region Buscar CargosSetores
    [HttpGet]

    public async Task<IActionResult> BuscarCargosSetoresAsync()
    {
        try
        {

            var resultado = await _service.BuscarCargosSetoresAsync();
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    #endregion

    #region Adicionar CargosSetores
    [HttpPost]

    public async Task<IActionResult> AdicionarCargosSetoresAsync([FromBody] CargosSetores cargosSetores)
    {
        try
        {

           
            
            var resultado = await _service.AdicionarCargosSetoresAsync(cargosSetores);
            
                return CustomResponse("CargosSetores criado com sucesso!!");
            

        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }

    }
    #endregion

    #region Excluir CargosSetores
    [HttpDelete]

    public async Task<IActionResult> ExcluirCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {

            

            var resultado = await _service.ExcluirCargosSetoresAsync(cargosSetores);
            return CustomResponse("CargosSetores excluído com sucesso!");


        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    #endregion

    #region Atualizar Cargos Setores
    [HttpPut]
    public async Task<IActionResult> AtualizarCargosSetoresAsync([FromBody] AtualizarCargosSetoresDto atualizarCargos)
    {
        try
        {

            var cargosSetoresAntigo = new CargosSetores
            { 
                CargosId = atualizarCargos.CargosIdAntigo,
                SetoresId = atualizarCargos.SetoresIdAntigo
            };

            var cargosSetoresNovo = new CargosSetores
            {
                CargosId = atualizarCargos.CargosIdNovo,
                SetoresId = atualizarCargos.SetoresIdNovo
            };


            var resultado = await _service.AtualizarCargosSetoresAsync(cargosSetoresNovo, cargosSetoresAntigo);
            return CustomResponse("CargosSetores atualizado com sucesso!");
        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }

    }
    #endregion

    #region Buscar cargosSetores por pagina

    [HttpGet("{pagina}/{quantidade}")]

    public async Task<IActionResult> BuscarCargosSetoresPaginado(int pagina, int quantidade)
    {
        try
        {
            var resultado = await _service.BuscarCargosSetoresPaginadoAsync(pagina, quantidade);

            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }

    }
    #endregion

    #region Buscar Inner Cargos Setores
    [HttpGet("inner")]
    public async Task<IActionResult> BuscarInnerCargosSetoresAsync()
    {
        try
        {

            var resultado = await _service.ListarInnerCargosSetoresAsync();
            return CustomResponse(resultado);
        }
        catch (Exception ex)
        {

            NotificarErro(ex.Message);
            return CustomResponse();
        }
    }
    #endregion
}
