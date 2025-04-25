using Domain.Dtos;
using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IHorariosRepository
    {
        Task<List<Horarios>> BuscarTodosHorariosAsync();
        Task<Horarios> BuscarHorarioPorIdAsync(int id);
        Task<RetornoPaginado<Horarios>> BuscarHorarioPaginadoAsync(int pagina, int quantidade);
        Task<bool> AdicionarHorarioAsync(HorariosDto horario);
        Task<bool> AtualizarHorarioAsync(int id, HorariosDto horario);
        Task<bool> ExcluirHorarioAsync(int id);
    }
}
