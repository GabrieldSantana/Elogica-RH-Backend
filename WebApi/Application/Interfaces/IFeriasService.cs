using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IFeriasService
    {
        Task<IEnumerable<Ferias>> BuscarFeriasAsync();
        Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade);
        Task<Ferias> BuscarFeriasPorIdAsync(int id);
        Task<Ferias> AdicionarFeriasAsync(Ferias ferias);
        Task AtualizarFeriasAsync(int id, FeriasDto dto);
        Task ExcluirFeriasAsync(int id);
    }
}