using Domain.Models;
using Domain.Dtos;

namespace Application.Interfaces
{
    public interface ICargosServices
    {
        Task<RetornoPaginado<Cargos>> BuscarCargosPaginadoAsync(int pagina, int quantidade);
        Task<IEnumerable<Cargos>> BuscarCargosAsync();
        Task<Cargos> BuscarCargosPorIdAsync(int id);
        Task<bool> AdicionarCargosAsync(CargosDto cargosDto);
        Task<bool> AtualizarCargosAsync(int id, AtualizarCargosDto cargosDto);
        Task<bool> ExcluirCargosAsync(int id);
    }
}