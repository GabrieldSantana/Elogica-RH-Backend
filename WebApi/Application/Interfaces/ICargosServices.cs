using Domain.Models;
using Domain.Dtos;

namespace Application.Interfaces
{
    public interface ICargosServices
    {
        Task<RetornoPaginado<Cargos>> BuscarCargosPaginadoAsync(int pagina, int quantidade);
        Task<IEnumerable<CargosDto>> BuscarCargosAsync();
        Task<Cargos> BuscarCargosPorIdAsync(int id);
        Task<int> AdicionarCargosAsync(CargosDto cargosDto);
        Task<bool> AtualizarCargosAsync(int id, AtualizarCargosDto cargosDto);
        Task<bool> ExcluirCargosAsync(int id);
    }
}