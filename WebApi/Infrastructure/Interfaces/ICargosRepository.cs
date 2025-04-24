using Domain.Models;

namespace Infrastructure.Interfaces
{
    interface ICargosRepository
    {
        Task<RetornoPaginado<Cargos>> BuscarCargosPaginadoAsync(int pagina, int quantidade);
        Task<IEnumerable<Cargos>> BuscarCargosAsync();
        Task<Cargos> BuscarCargosPorIdAsync(int id);
        Task<int> AdicionarCargosAsync(Cargos cargos);
        Task<bool> AtualizarCargosAsync(Cargos cargos);
        Task<bool> ExcluirCargosAsync(int id);
    }
}
