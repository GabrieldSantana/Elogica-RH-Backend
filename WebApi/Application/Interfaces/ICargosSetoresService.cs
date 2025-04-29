using Domain.Models;

namespace Application.Interfaces
{
    public interface ICargosSetoresService
    {

        Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetoresNovo, CargosSetores cargosSetoresAntigo);
        Task<List<CargosSetores>> BuscarCargosSetoresAsync();
        Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
        Task<bool> ExcluirCargosSetoresAsync(CargosSetores cargosSetores);
        Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int pagina, int quantidade);
        Task<List<InnerCargoSetor>> ListarInnerCargosSetoresAsync();
    }
}
