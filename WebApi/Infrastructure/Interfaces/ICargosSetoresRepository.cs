
using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ICargosSetoresRepository
{
    Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetoresNovo, CargosSetores cargosSetoresAntigo);
    Task<List<CargosSetores>> BuscarCargosSetoresAsync();
    Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
    Task<bool> ExcluirCargosSetoresAsync(CargosSetores cargosSetores );
    Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina);
    Task<bool> VerificarCargosSetores(CargosSetores cargosSetores);



    
}
