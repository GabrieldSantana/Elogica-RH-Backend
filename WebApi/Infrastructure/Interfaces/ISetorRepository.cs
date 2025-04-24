using Domain;
using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ISetorRepository
{
    Task<IEnumerable<Setor>> BuscarTodosSetoresAsync();
    Task<RetornoPaginado<Setor>> BuscarSetoresPorPaginaAsync(int pagina, int quantidade);
    Task<Setor> BuscarSetoresPorIdAsync(int id);
    Task<Setor> AdicionarSetoresAsync(Setor setor);
    Task<Setor> AtualizarSetoresAsync(Setor setor);
    Task<bool> RemoverSetoresAsync(int id);
  
}
