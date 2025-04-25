using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("ferias")]
    public class FeriasController : MainController
    {
        private readonly IFeriasService _feriasService;

        public FeriasController(IFeriasService feriasService, INotificador notificador)
            : base(notificador)
        {
            _feriasService = feriasService;
        }

        [HttpGet("{pagina}/{quantidade}")]
        public async Task<IActionResult> BuscarFeriasPaginadoAsync(
             int pagina = 1,
             int quantidade = 10)
        {
            try
            {
                var retorno = await _feriasService.BuscarFeriasPaginadoAsync(pagina, quantidade);
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro($"Ocorreu um erro ao buscar férias paginadas: {ex.Message}");
                return CustomResponse();
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarFeriasAsync()
        {
            try
            {
                var retorno = await _feriasService.BuscarFeriasAsync();
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro($"Ocorreu um erro ao buscar férias: {ex.Message}");
                return CustomResponse();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarFeriasPorIdAsync(int id)
        {
            try
            {
                var ferias = await _feriasService.BuscarFeriasPorIdAsync(id);
                if (ferias == null)
                {
                    NotificarErro("Férias não encontradas");
                    return CustomResponse();
                }
                return CustomResponse(ferias);
            }
            catch (Exception ex)
            {
                NotificarErro($"Ocorreu um erro ao buscar férias por ID: {ex.Message}");
                return CustomResponse();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarFeriasAsync([FromBody] FeriasDto feriasDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResponse(ModelState);
                }

                var ferias = new Ferias
                {
                    DataInicio = feriasDto.DataInicio,
                    DataFim = feriasDto.DataFim,
                    FuncionarioId = feriasDto.FuncionarioId
                };

                var retorno = await _feriasService.AdicionarFeriasAsync(ferias);
                return CustomResponse(retorno);
            }
            catch (ValidationException ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFeriasAsync(int id, [FromBody] FeriasDto dto)
        {
            try
            {
                await _feriasService.AtualizarFeriasAsync(id, dto);
                return CustomResponse();
            }
            catch (ValidationException ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirFeriasAsync(int id)
        {
            try
            {
                await _feriasService.ExcluirFeriasAsync(id);
                return CustomResponse();
            }
            catch (ValidationException ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }

        }
    }
}