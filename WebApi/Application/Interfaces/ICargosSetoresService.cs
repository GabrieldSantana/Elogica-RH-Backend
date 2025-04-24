using Domain.Models;

namespace Application.Interfaces
{
    public interface ICargosSetoresService
    {

        Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<List<CargosSetores>> BuscarCargosSetoresAsync();
        Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<bool> ExcluirCargosSetoresAsync(CargosSetores cargosSetores);

        Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina);
    }
}
