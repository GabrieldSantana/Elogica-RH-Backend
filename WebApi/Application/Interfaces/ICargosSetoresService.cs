using Domain.Models;

namespace Application.Interfaces
{
    public interface ICargosSetoresService
    {

        Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetoresNovo, CargosSetores cargosSetoresAntigo);
        Task<List<CargosSetores>> BuscarCargosSetoresAsync();
        Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<bool> ExcluirCargosSetoresAsync(int cargosId);

        Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina);
    }
}
