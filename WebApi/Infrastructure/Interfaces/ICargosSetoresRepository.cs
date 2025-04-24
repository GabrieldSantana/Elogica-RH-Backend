
using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ICargosSetoresRepository
{
    Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores);
    Task<List<CargosSetores>> BuscarCargosSetoresAsync();
    Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores);
    Task<bool> ExcluirCargosSetoresAsync(int id );

    Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina);


    
}
