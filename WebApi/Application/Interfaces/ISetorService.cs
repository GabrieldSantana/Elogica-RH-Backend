using Domain.Models;

namespace Application.Interfaces;

public interface ISetorService
{
    Task<IEnumerable<Setor>> BuscarSetoresAsync();
    Task<RetornoPaginado<Setor>>BuscarSetoresPaginadoAsync();
    Task<Setor> BuscarSetorPorIdAsync(int id);
    Task<bool> AdicionarSetoresAsync(Setor setor);
    Task<bool> AtualizarSetoresAsync(Setor setor);
    Task<bool> DeletarSetoresAsync(int id);

}
