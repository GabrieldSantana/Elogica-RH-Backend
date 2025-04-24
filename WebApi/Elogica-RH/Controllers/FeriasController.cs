using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using Domain.Main;
using AutoMapper;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeriasController : MainController
    {
        private readonly IFeriasService _feriasService;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        public FeriasController(INotificador notificador, IFeriasService feriasService, IMapper mapper) : base(notificador)
        {
            _notificador = notificador;
            _feriasService = feriasService;
            _mapper = mapper;
        }

        [HttpGet("paginado")]


        public async Task<IActionResult> BuscarFeriasPaginadoAsync([FromQuery] int pagina = 1, [FromQuery] int quantidade = 10)
        {
            try
            {
                if (pagina < 1 || quantidade < 1)
                    return CustomResponse(ModelState);

                var resultado = await _feriasService.BuscarFeriasPaginadoAsync(pagina, quantidade);
                return CustomResponse(resultado);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarFeriasAsync() => Ok(await _feriasService.BuscarFeriasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarFeriasPorIdAsync(int id)
        {
            try
            {
                var ferias = await _feriasService.BuscarFeriasPorIdAsync(id);
                if (ferias == null) return NotFound();
                return CustomResponse(ferias);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
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

                var result = await _feriasService.AdicionarFeriasAsync(feriasDto);
                return CustomResponse(nameof(BuscarFeriasPorIdAsync), new { id = result.Id }, result);
            }
            catch (ValidationException ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFeriasAsync(int id, [FromBody] Ferias dto)
        {
            if (id != dto.Id) return CustomResponse();
            await _feriasService.AtualizarFeriasAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirFeriasAsync(int id)
        {
            await _feriasService.ExcluirFeriasAsync(id);
            return NoContent();

        }

    }
}