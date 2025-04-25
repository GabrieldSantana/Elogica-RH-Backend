using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Elogica_RH.Controllers
{
    [Route("api/horarios")]
    [ApiController]
    public class HorariosController : MainController
    {
        private readonly IHorariosService _service;
        private readonly IMapper _mapper;

        public HorariosController(IHorariosService service, IMapper mapper, INotificador notificador)
            : base(notificador)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost()]
        public async Task<IActionResult> AdicionarHorario([FromBody] HorariosDto dto)
        {
            try
            {
                var retorno = await _service.AdicionarHorarioAsync(dto);
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarHorario(int id, [FromBody] HorariosDto dto)
        {
            try
            {
                var retorno = await _service.AtualizarHorarioAsync(id, dto);
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirHorario(int id)
        {
            try
            {
                var retorno = await _service.ExcluirHorarioAsync(id);
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarPorId(int id)
        {
            try
            {
                var retorno = await _service.BuscarHorarioPorIdAsync(id);
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet()]
        public async Task<ActionResult> BuscarTodos()
        {
            try
            {
                var retorno = await _service.BuscarTodosHorariosAsync();
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("paginado")]
        public async Task<ActionResult> BuscarPaginado([FromQuery] int pagina = 1, [FromQuery] int quantidade = 10)
        {
            try
            {
                var retorno = await _service.BuscarHorarioPaginadoAsync(pagina, quantidade);
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }
    }
}
