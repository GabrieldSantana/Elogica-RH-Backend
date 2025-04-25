
using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IHorariosService
    {
        Task<List<Horarios>> BuscarHorariosAsync();
        Task<Horarios> BuscarHorarioPorIdAsync(int id);
        Task<RetornoPaginado<Horarios>> BuscarHorarioPaginadoAsync(int pagina, int quantidade);
        Task<string> AdicionarHorarioAsync(HorariosDto horario);
        Task<string> AtualizarHorarioAsync(int id, HorariosDto horario);
        Task<string> ExcluirHorarioAsync(int id);
    }
}
