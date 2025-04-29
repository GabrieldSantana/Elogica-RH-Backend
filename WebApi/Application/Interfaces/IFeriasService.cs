using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IFeriasService
    {
        Task<IEnumerable<Ferias>> BuscarFeriasAsync();
        Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade);
        Task<Ferias> BuscarFeriasPorIdAsync(int id);
        Task<bool> AdicionarFeriasAsync(FeriasDto ferias);
        Task<Ferias> AtualizarFeriasAsync(int id, FeriasDto dto);
        Task<(bool Success, string Message)> ExcluirFeriasAsync(int id);

    }
}