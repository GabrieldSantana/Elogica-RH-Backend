using Domain.Models;

namespace Application.Interfaces
{
    public interface ICargosSetoresService
    {

        Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<List<CargosSetores>> BuscarCargosSetoresAsync();
        Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<bool> ExcluirCargosSetoresAsync(int cargosId);

        Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina);
    }
}
